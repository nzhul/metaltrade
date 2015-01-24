using Application.Models;
using Application.Web.Areas.Administration.InputModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Application.Web.Areas.Administration.Models.ViewModels;
using System.Net;
using System.IO;
using ImageResizer;
using Application.Web.Areas.Administration.Models.InputModels;
using Utilities;

namespace Application.Web.Areas.Administration.Controllers
{
    public class ProductsController : BaseController
    {
        private IEnumerable<SelectListItem> GetCategories()
        {
            var categories = this.Data.Categories
                .All()
                .OrderBy(x => x.Id)
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name.ToString()
                });

            return new SelectList(categories, "Value", "Text");
        }
        private IEnumerable<SubCategory> InitSubCategories()
        {
            var subCategories = this.Data.SubCategories
                .All()
                .OrderBy(x => x.Id)
                .ToList();
                return subCategories;
        }

        const int PageSize = 10;

        // GET: Administration/Products
        [HttpGet]
        public ActionResult Index(int? id)
        {
            int pageNumber = id.GetValueOrDefault(1);
            var productsList = this.Data.Products.All()
                .OrderBy(x => x.DisplayOrder)
                .Skip((pageNumber - 1) * PageSize).Take(PageSize)
                .AsEnumerable()
                .Select(x => new ProductViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    ShortDescription = x.ShortDescription,
                    CategoryName = x.Category.Name,
                    IsActive = x.IsActive,
                    IsFeatured = x.IsFeatured,
                    DateAdded = x.DateAdded,
                    PrimaryImage = x.Images.First(image => image.IsPrimary == true),
                    Slug = x.Slug
                }).ToList();

            ViewBag.Pages = Math.Ceiling((double)this.Data.Products.All().Count() / PageSize);
            ViewBag.CurrentPage = id;
            return View(productsList);
        }

        public JsonResult GetSubCategories(string id)
        {
            var categoryId = int.Parse(id);
            var subcategories = this.Data.SubCategories
                .All()
                .OrderBy(x => x.DisplayOrder)
                .Where(x => x.CategoryId == categoryId)
                .Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    }).ToList();


            return Json(new SelectList(subcategories, "Value", "Text"));
        }

        public JsonResult GetAllTags()
        {
            var tags = this.Data.Tags
                .All()
                .OrderBy(x => x.Id)
                .Select(x => x.Name).ToList();

            return Json(tags, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new CreateProductInputModel();
            model.Categories = GetCategories();
            model.AvailableSubCategories = InitSubCategories();
            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateProductInputModel product)
        {
            if (ModelState.IsValid)
            {
                var currentUserId = this.User.Identity.GetUserId();
                var selectedCategory = this.Data.Categories.Find(product.SelectedCategoryId);
                string slug = SlugGenerator.Generate(product.Name);
                var newProduct = new Product
                {
                    ApplicationUserId = currentUserId,
                    BulletsText = product.BulletsText,
                    DateAdded = DateTime.Now,
                    IsActive = product.IsActive,
                    IsFeatured = product.IsFeatured,
                    LongDescription = product.LongDescription,
                    Name = product.Name,
                    ShortDescription = product.ShortDescription,
                    Category = selectedCategory,
                    Slug = slug
                };

                // Add the tags
                foreach (var tag in product.Tags)
                {
                    var tagDb = this.Data.Tags
                        .All()
                        .Where(x => x.Name == tag).FirstOrDefault();
                    if (tagDb != null)
                    {
                        newProduct.Tags.Add(tagDb);
                    }
                    else
                    {
                        var newTag = new Tag
                        {
                            Name = tag
                        };
                        this.Data.Tags.Add(newTag);
                        this.Data.SaveChanges();
                        newProduct.Tags.Add(newTag);
                    }
                    this.Data.SaveChanges();
                }

                for (int i = 0; i < product.SelectedSubCategoriesIds.Count; i++)
                {
                    var theSubCategory = this.Data.SubCategories.Find(product.SelectedSubCategoriesIds[i]);
                    newProduct.SubCategories.Add(theSubCategory);
                }

                var defaultImage = new Image
                {
                    ImageExtension = "jpg",
                    ImagePath = "Content\\images\\noimage\\no-image",
                    IsPrimary = true,
                    DateAdded = DateTime.Now
                };

                
                this.Data.Products.Add(newProduct);
                this.Data.SaveChanges();
                newProduct.Images.Add(defaultImage);
                this.Data.SaveChanges();
                TempData["message"] = "Успещно добави <strong>нов продукт</strong> с име: " + product.Name;
                TempData["messageType"] = "success";
                return RedirectToAction("Index");
            }

            product.Categories = GetCategories();
            product.AvailableSubCategories = InitSubCategories();
            TempData["message"] = "Невалидни данни за продукта!<br/> Моля попълнете <strong>всички</strong> полета в червено!";
            TempData["messageType"] = "danger";
            return View(product);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var productDb = this.Data.Products.Find(id);
            if (productDb == null)
            {
                return HttpNotFound();
            }
            var model = new CreateProductInputModel
            {
                Id = productDb.Id,
                BulletsText = productDb.BulletsText,
                Categories = GetCategories(),
                IsActive = productDb.IsActive,
                IsFeatured = productDb.IsFeatured,
                LongDescription = productDb.LongDescription,
                Name = productDb.Name,
                SelectedCategoryId = productDb.Category.Id,
                ShortDescription = productDb.ShortDescription,
                Tags = productDb.Tags.Select(x=>x.Name).ToList(),
                Images = productDb.Images,
                DisplayOrder = productDb.DisplayOrder,
                Slug = productDb.Slug
            };

            model.AvailableSubCategories = InitSubCategories();

            model.SelectedSubCategoriesIds = new List<int>();
            foreach (var subCategory in productDb.SubCategories)
            {
                model.SelectedSubCategoriesIds.Add(subCategory.Id);
            }
            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CreateProductInputModel product)
        {
            if (ModelState.IsValid)
            {
                var dbProduct = this.Data.Products.Find(product.Id);
                if (dbProduct == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                dbProduct.BulletsText = product.BulletsText;
                dbProduct.Category = this.Data.Categories.Find(product.SelectedCategoryId);
                dbProduct.IsActive = product.IsActive;
                dbProduct.IsFeatured = product.IsFeatured;
                dbProduct.LongDescription = product.LongDescription;
                dbProduct.Name = product.Name;
                dbProduct.ShortDescription = product.ShortDescription;
                dbProduct.DisplayOrder = product.DisplayOrder;
                dbProduct.Slug = product.Slug;

                // Remove all Tags
                foreach (var tag in dbProduct.Tags.ToList())
                {
                    dbProduct.Tags.Remove(tag);
                }
                this.Data.SaveChanges();

                // Add the tags again
                foreach (var tag in product.Tags)
                {
                    var tagDb = this.Data.Tags
                        .All()
                        .Where(x => x.Name == tag).FirstOrDefault();
                    if (tagDb != null)
                    {
                        dbProduct.Tags.Add(tagDb);
                    }
                    else
                    {
                        var newTag = new Tag
                        {
                            Name = tag
                        };
                        this.Data.Tags.Add(newTag);
                        this.Data.SaveChanges();
                        dbProduct.Tags.Add(newTag);
                    }
                    this.Data.SaveChanges();
                }

                // Delete all SubCategories
                foreach (var subCategory in dbProduct.SubCategories.ToList())
                {
                    dbProduct.SubCategories.Remove(subCategory);
                }
                this.Data.SaveChanges();
                // Insert The New SubCategories
                for (int i = 0; i < product.SelectedSubCategoriesIds.Count; i++)
                {
                    var theSubCategory = this.Data.SubCategories.Find(product.SelectedSubCategoriesIds[i]);
                    dbProduct.SubCategories.Add(theSubCategory);
                }

                this.Data.SaveChanges();
                TempData["message"] = "Продукта беше <strong>редактиран</strong> успешно <strong><a href='/Products/Details/" + dbProduct.Id +"'>ПРЕГЛЕДАЙ ГО!</a></strong>";
                TempData["messageType"] = "success";
                return RedirectToAction("Index");
            }

            product.Categories = GetCategories();
            product.AvailableSubCategories = InitSubCategories();
            TempData["message"] = "Невалидни данни за продукта!<br/> Моля попълнете <strong>всички</strong> полета в червено!";
            TempData["messageType"] = "danger";
            return View(product);
        }


        [HttpPost]
        public ActionResult DeleteProduct(int id)
        {
            this.Data.Products.Delete(id);
            this.Data.SaveChanges();
            return Content("<tr><td class='text-center' style='padding:15px;'><span class='label label-success'>Изтрито успешно!</span></td></tr>");
        }

        [HttpPost]
        public ActionResult Upload(UploadPhotoModel uploadData)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Dictionary<string, string> versions = new Dictionary<string, string>();
                    //Define the versions to generate
                    versions.Add("_indexThumb", "width=230&height=234&crop=auto&format=jpg"); //Crop to square thumbnail
                    versions.Add("_detailsBigThumb", "maxwidth=336&crop=auto&format=jpg"); //Fit inside 400x400 area, jpeg
                    versions.Add("_detailsSmallThumb", "width=77&height=61&crop=auto&format=jpg"); //Fit inside 400x400 area, jpeg
                    versions.Add("_large", "maxwidth=1500&maxheight=1500&format=jpg"); //Fit inside 1900x1200 area

                    int categoryId = uploadData.CategoryId;
                    int productId = uploadData.ProductId;
                    var theProduct = this.Data.Products.Find(productId);

                    bool firstLoop = true;
                    foreach (var file in uploadData.Files)
                    {
                        if (file != null)
                        {
                            var originalFileName = file.FileName.Split('.')[0].Replace(' ', '_');
                            var originalFileExtension = file.FileName.Split('.')[1];

                            string uploadFolder = System.Web.HttpContext.Current.Server.MapPath("~/Uploads/" + categoryId + "/" + productId);
                            if (!Directory.Exists(uploadFolder)) Directory.CreateDirectory(uploadFolder);

                            foreach (string suffix in versions.Keys)
                            {
                                //Int32 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                                string fileName = Path.Combine(uploadFolder, originalFileName + suffix);

                                fileName = ImageBuilder.Current.Build(file, fileName, new ResizeSettings(versions[suffix]), false, true);
                            }

                            var newImage = new Image
                                {
                                    ImagePath = "Uploads\\" + categoryId + "\\" + productId + "\\" + originalFileName,
                                    ImageExtension = originalFileExtension,
                                    IsPrimary = false,
                                    DateAdded = DateTime.Now
                                };

                            if (firstLoop)
                            {
                                theProduct.Images.First(image => image.IsPrimary == true).IsPrimary = false;
                                newImage.IsPrimary = true;
                            }
                            
                            theProduct.Images.Add(newImage);
                            this.Data.SaveChanges();
                        }

                        firstLoop = false;
                    }
                }

                TempData["message"] = "Снимката беше <strong>добавена</strong> успешно!";
                TempData["messageType"] = "success";
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["message"] = "Неуспешно качване на снимка!<br/> Моля свържете се с администратор!";
                TempData["messageType"] = "danger";
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ActionResult MakePrimary(int imageId, int productId)
        {
            var theProduct = this.Data.Products.Find(productId);
            if (theProduct == null)
            {
                return HttpNotFound();
            }

            var oldPrimary = theProduct.Images.FirstOrDefault(image => image.IsPrimary);
            oldPrimary.IsPrimary = false;

            var newPrimary = theProduct.Images.FirstOrDefault(image => image.Id == imageId);
            newPrimary.IsPrimary = true;

            this.Data.SaveChanges();

            return RedirectToAction("Edit", new { id = productId });
        }

        [HttpPost]
        public ActionResult DeleteImage(int imageId)
        {
            var theImage = this.Data.Images.Find(imageId);
            string file1 = System.Web.HttpContext.Current.Server.MapPath("~/" + theImage.ImagePath + "_detailsBigThumb.jpg");
            string file2 = System.Web.HttpContext.Current.Server.MapPath("~/" + theImage.ImagePath + "_detailsSmallThumb.jpg");
            string file3 = System.Web.HttpContext.Current.Server.MapPath("~/" + theImage.ImagePath + "_indexThumb.jpg");
            string file4 = System.Web.HttpContext.Current.Server.MapPath("~/" + theImage.ImagePath + "_large.jpg");

            TryToDelete(file1);
            TryToDelete(file2);
            TryToDelete(file3);
            TryToDelete(file4);

            this.Data.Images.Delete(imageId);
            this.Data.SaveChanges();

            return Content(@"<span class='label label-success'>Изтрито успешно!</span>");

        }

        private bool TryToDelete(string filePath)
        {
            try
            {
                System.IO.File.Delete(filePath);
                return true;
            }
            catch (IOException)
            {
                return false;
            }
        }
    }
}
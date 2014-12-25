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

        private IEnumerable<SelectListItem> GetSubCategories()
        {
            var categories = this.Data.SubCategories
                .All()
                .OrderBy(x => x.Id)
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name.ToString()
                });

            return new SelectList(categories, "Value", "Text");
        }

        private IEnumerable<Image> GetImages(int productId)
        {
            var images = this.Data.Images
                .All()
                .OrderBy(x => x.Id)
                .Where(x =>x.ProductId == productId);

            return new List<Image>(images);
        }

        // GET: Administration/Products
        [HttpGet]
        public ActionResult Index()
        {
            var productsList = this.Data.Products.All()
                .OrderByDescending(x => x.DateAdded)
                .AsEnumerable()
                .Select(x => new ProductViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    ShortDescription = x.ShortDescription,
                    CategoryName = x.CategoryName,
                    SubCategoryName = x.SubCategory.Name,
                    IsActive = x.IsActive,
                    IsFeatured = x.IsFeatured,
                    DateAdded = x.DateAdded
                }).ToList();
            return View(productsList);
        }

        public JsonResult GetSubCategories(string id)
        {
            var categoryId = int.Parse(id);
            var subcategories = this.Data.SubCategories
                .All()
                .OrderBy(x => x.DateAdded)
                .Where(x => x.CategoryId == categoryId)
                .Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    }).ToList();


            return Json(new SelectList(subcategories, "Value", "Text"));
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new CreateProductInputModel();
            model.Categories = GetCategories();
            model.SubCategories = GetSubCategories();
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
                var newProduct = new Product
                {
                    ApplicationUserId = currentUserId,
                    BulletsText = product.BulletsText,
                    DateAdded = DateTime.Now,
                    Images = null,
                    IsActive = product.IsActive,
                    IsFeatured = product.IsFeatured,
                    LongDescription = product.LongDescription,
                    Name = product.Name,
                    ShortDescription = product.ShortDescription,
                    CategoryName = selectedCategory.Name,
                    CategoryId = selectedCategory.Id,
                    SubCategoryId = product.SelectedSubCategoryId,
                    Tags = null
                };
                this.Data.Products.Add(newProduct);
                this.Data.SaveChanges();
                TempData["message"] = "Успещно добави <strong>нов продукт</strong> с име: " + product.Name;
                TempData["messageType"] = "success";
                return RedirectToAction("Index");
            }

            product.Categories = GetCategories();
            product.SubCategories = GetSubCategories();
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
                SelectedCategoryId = productDb.CategoryId,
                SelectedSubCategoryId = productDb.SubCategoryId,
                ShortDescription = productDb.ShortDescription,
                SubCategories = GetSubCategories(),
                Tags = GetTagsAsString(id),
                Images = GetImages(productDb.Id)
            };

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
                dbProduct.CategoryId = product.SelectedCategoryId;
                dbProduct.CategoryName = this.Data.Categories.Find(product.SelectedCategoryId).Name;
                dbProduct.IsActive = product.IsActive;
                dbProduct.IsFeatured = product.IsFeatured;
                dbProduct.LongDescription = product.LongDescription;
                dbProduct.Name = product.Name;
                dbProduct.ShortDescription = product.ShortDescription;
                dbProduct.SubCategoryId = product.SelectedSubCategoryId;

                this.Data.SaveChanges();
                TempData["message"] = "Продукта беше <strong>редактиран</strong> успешно <strong><a href='#'>ПРЕГЛЕДАЙ ГО!</a></strong>";
                TempData["messageType"] = "success";
                return RedirectToAction("Index");
            }

            product.Categories = GetCategories();
            product.SubCategories = GetSubCategories();
            TempData["message"] = "Невалидни данни за продукта!<br/> Моля попълнете <strong>всички</strong> полета в червено!";
            TempData["messageType"] = "danger";
            return View(product);
        }

        private string GetTagsAsString(int? id)
        {
            return "not implemented!";
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

                    foreach (var file in uploadData.Files)
                    {
                        if (file != null)
                        {
                            var originalFileName = file.FileName.Split('.')[0].Replace(' ', '_');
                            var originalFileExtension = file.FileName.Split('.')[1];
                            int categoryId = uploadData.CategoryId;
                            int subCategoryId = uploadData.SubCategoryId;
                            int productId = uploadData.ProductId;

                            string uploadFolder = System.Web.HttpContext.Current.Server.MapPath("~/Uploads/" + categoryId + "/" + subCategoryId + "/" + productId);
                            if (!Directory.Exists(uploadFolder)) Directory.CreateDirectory(uploadFolder);

                            foreach (string suffix in versions.Keys)
                            {
                                //Int32 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                                string fileName = Path.Combine(uploadFolder, originalFileName + suffix);

                                fileName = ImageBuilder.Current.Build(file, fileName, new ResizeSettings(versions[suffix]), false, true);
                            }

                            var newImage = new Image
                                {
                                    ImagePath = "Uploads\\" + categoryId + "\\" + subCategoryId + "\\" + productId + "\\" + originalFileName,
                                    ImageExtension = originalFileExtension,
                                    IsPrimary = false,
                                    ProductId = productId
                                };
                            this.Data.Images.Add(newImage);
                            this.Data.SaveChanges();
                        }
                    }
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
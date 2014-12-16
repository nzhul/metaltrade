using Application.Models;
using Application.Web.Areas.Administration.InputModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Application.Web.Areas.Administration.Models.ViewModels;

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

        [HttpGet]
        public ActionResult Create()
        {
            var model = new CreateProductInputModel();
            model.Categories = GetCategories();
            model.SubCategories = GetSubCategories();
            return this.View(model);
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateProductInputModel product)
        {
            if (ModelState.IsValid)
            {
                var currentUserId = this.User.Identity.GetUserId();
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
                    CategoryName = this.Data.Categories.Find(product.SelectedCategoryId).Name,
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

        [HttpPost]
        [Authorize]
        public ActionResult DeleteProduct(int id)
        {
            System.Threading.Thread.Sleep(3000);
            this.Data.Products.Delete(id);
            this.Data.SaveChanges();
            return Content("<tr><td class='text-center' style='padding:15px;'><span class='label label-success'>Изтрито успешно!</span></td></tr>");
        }
    }
}
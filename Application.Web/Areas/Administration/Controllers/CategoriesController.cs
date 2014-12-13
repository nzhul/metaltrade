using Application.Models;
using Application.Web.Areas.Administration.Models.InputModels;
using Application.Web.Areas.Administration.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Application.Web.Areas.Administration.Controllers
{
    public class CategoriesController : BaseController
    {
        // GET: Administration/Categories
        public ActionResult Index()
        {
            var categoriesList = this.Data.Categories.All()
                .OrderByDescending(x => x.DateAdded)
                .AsEnumerable()
                .Select(x => new CategoryViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    SubCategories = this.Data.SubCategories
                        .All()
                        .OrderBy(y => y.Id)
                        .Where(y => y.CategoryId == x.Id)
                        .Select(y => new SubCategoryViewModel
                        {
                            Id = y.Id,
                            Name = y.Name
                        })
                }).ToList();
            return View(categoriesList);
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult PostSubCategory(SubCategoryInputModel subcategoryModel)
        {
            if (ModelState.IsValid)
            {
                var newSubCategory = new SubCategory()
                {
                    CategoryId = subcategoryModel.CategoryId,
                    Name = subcategoryModel.Name,
                    Description = subcategoryModel.Description
                };
                this.Data.SubCategories.Add(newSubCategory);
                this.Data.SaveChanges();

                var viewModel = new SubCategoryViewModel
                {
                    Id = newSubCategory.Id,
                    Name = newSubCategory.Name,
                    CategoryId = newSubCategory.CategoryId,
                    Description = newSubCategory.Description
                };
                return PartialView("_SubCategoryPartial", viewModel);
            }
            else
            {
                // HttpResponceMessage needs using: using System.Net.Http;
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest, ModelState.Values.First().ToString());
            }
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult PostCategory(CategoryInputModel categoryModel)
        {
            if (ModelState.IsValid)
            {
                var newCategory = new Category()
                {
                    Name = categoryModel.Name,
                    Description = categoryModel.Description,
                    DateAdded = DateTime.Now
                };
                this.Data.Categories.Add(newCategory);
                this.Data.SaveChanges();

                var viewModel = new CategoryViewModel
                {
                    Id = newCategory.Id,
                    Name = newCategory.Name,
                    Description = newCategory.Description
                };
                return PartialView("_CategoryPartial", viewModel);
            }
            else
            {
                // HttpResponceMessage needs using: using System.Net.Http;
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest, ModelState.Values.First().ToString());
            }
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult EditSubCategory(SubCategoryInputModel subcategoryModel)
        {
            if (ModelState.IsValid)
            {
                var theSubCategory = this.Data.SubCategories.Find(subcategoryModel.Id);
                theSubCategory.Name = subcategoryModel.Name;
                theSubCategory.Description = subcategoryModel.Description;
                this.Data.SaveChanges();

                var contentToReturn = "<span id='" + "subcategory-span-" + subcategoryModel.Id + "'>" + subcategoryModel.Name+ "</span>";

                return Content(contentToReturn);
            }
            else
            {
                // HttpResponceMessage needs using: using System.Net.Http;
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest, ModelState.Values.First().ToString());
            }
        }

        public ActionResult DeleteSubCategory(int id)
        {
            this.Data.SubCategories.Delete(id);
            this.Data.SaveChanges();
            return Content("<span class='label label-success'>Изтрито успешно!</span>");
        }

        public ActionResult DeleteCategory(int id)
        {
            var theCategory = this.Data.Categories.Find(id);
            foreach (var subCategory in theCategory.SubCategories.ToList())
            {
                this.Data.SubCategories.Delete(subCategory);
            }
            this.Data.Categories.Delete(id);
            this.Data.SaveChanges();
            return Content(@"<div class='alert alert-dismissable alert-danger'>
                              <button type='button' class='close' data-dismiss='alert'>×</button>
                              <strong>Категорията беше изтрита успешно!</strong></br>
                              Всички подкатегории и продукти които са към тази категория - също бяха изтрити безвъзвратно.
                              Няма връшане назад :)
                            </div>");
        }
    }
}
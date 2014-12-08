﻿using Application.Models;
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
                .OrderBy(x => x.Id)
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
                    Name = subcategoryModel.Name
                };
                this.Data.SubCategories.Add(newSubCategory);
                this.Data.SaveChanges();

                var viewModel = new SubCategoryViewModel
                {
                    Id = newSubCategory.Id,
                    Name = newSubCategory.Name
                };
                return PartialView("_SubCategoryPartial", viewModel);
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
            return Content("success");
        }
    }
}
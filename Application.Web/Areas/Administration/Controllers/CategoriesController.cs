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
    }
}
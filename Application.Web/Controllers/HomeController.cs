using Application.Web.Areas.Administration.Models.ViewModels;
using Application.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Application.Web.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Administration/Products
        [HttpGet]
        public ActionResult Index()
        {
            var model = new HomeViewModel();
            model.Categories = GetAllCategories();
            model.Products = GetFeaturedProducts();
            return View(model);
        }

        private IEnumerable<ProductViewModel> GetFeaturedProducts()
        {
            return this.Data.Products
                .All()
                .Where(x => x.IsActive == true)
                .Where(x => x.IsFeatured == true)
                .OrderBy(x => x.DateAdded)
                .Take(9)
                .AsEnumerable()
                .Select(x => new ProductViewModel
                {
                    CategoryName = x.CategoryName,
                    DateAdded = x.DateAdded,
                    Id = x.Id,
                    Name = x.Name,
                    PrimaryImage = x.Images.First(image => image.IsPrimary),
                    ShortDescription = x.ShortDescription,
                    SubCategoryName = x.SubCategory.Name
                }).ToList();
        }

        private IEnumerable<CategoryViewModel> GetAllCategories()
        {
            return this.Data.Categories
                .All()
                .OrderBy(x => x.Id)
                .AsEnumerable()
                .Select(x => new CategoryViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    SubCategories = this.Data.SubCategories
                        .All()
                        .OrderBy(y => y.DateAdded)
                        .Where(y => y.CategoryId == x.Id)
                        .Select(y => new SubCategoryViewModel
                        {
                            Id = y.Id,
                            Name = y.Name,
                            Description = y.Description
                        })
                }).ToList();

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
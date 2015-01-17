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
            var model = new ProductsViewModel();
            model.Products = GetFeaturedProducts();
            model.Articles = GetLatestArticles();
            return View(model);
        }

        private IEnumerable<ArticleViewModel> GetLatestArticles()
        {
            return this.Data.Articles
                .All()
                .OrderByDescending(x => x.DateCreated)
                .Take(3)
                .AsEnumerable()
                .Select(x => new ArticleViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    ShortDescription = x.ShortDescription
                }).ToList();
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
                    CategoryId = x.Category.Id,
                    CategoryName = x.Category.Name,
                    DateAdded = x.DateAdded,
                    Id = x.Id,
                    Name = x.Name,
                    PrimaryImage = x.Images.First(image => image.IsPrimary),
                    ShortDescription = x.ShortDescription
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
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Application.Data;
using Application.Models;
using Application.Web.Areas.Administration.Models.ViewModels;
using Application.Web.Models;

namespace Application.Web.Controllers
{
    public class ProductsController : BaseController
    {

        // GET: Products
        public ActionResult Index(int? Category, int? SubCategory, int? Page)
        {
            var model = new ProductsViewModel();
            model.Products = GetProducts(Category, SubCategory, Page);
            return View(model);
        }

        private IEnumerable<ProductViewModel> GetProducts(int? categoryId, int? subCategoryId, int? page)
        {
            var productsQuerable = this.Data.Products
                .All()
                .OrderBy(x => x.DateAdded)
                .AsQueryable();

            if (categoryId != null)
            {
                productsQuerable = productsQuerable.Where(x => x.Category.Id == categoryId);
            }
            if (subCategoryId != null)
            {
                productsQuerable = productsQuerable.Where(x => x.SubCategories.Any(subcategory => subcategory.Id == subCategoryId));
            }

            if (page != null)
            {
                // TODO;
            }

            var products = productsQuerable
                .ToList()
                .Select(x => new ProductViewModel
                {
                    CategoryId = x.Category.Id,
                    CategoryName = x.Category.Name,
                    DateAdded = x.DateAdded,
                    Id = x.Id,
                    Name = x.Name,
                    PrimaryImage = x.Images.First(image => image.IsPrimary),
                    ShortDescription = x.ShortDescription
                });

            return products;

            //return this.Data.Products
            //    .All()
            //    .Where(x => x.IsActive == true)
            //    .OrderBy(x => x.DateAdded)
            //    .Take(9)
            //    .AsEnumerable()
            //    .Select(x => new ProductViewModel
            //    {
            //        CategoryId = x.Category.Id,
            //        CategoryName = x.Category.Name,
            //        DateAdded = x.DateAdded,
            //        Id = x.Id,
            //        Name = x.Name,
            //        PrimaryImage = x.Images.First(image => image.IsPrimary),
            //        ShortDescription = x.ShortDescription
            //    }).ToList();
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //Product product = this.Data.Products.Find(id);
            //if (product == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(product);
            return View();
        }

    }
}

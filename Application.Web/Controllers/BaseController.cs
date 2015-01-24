using Application.Data;
using Application.Web.Areas.Administration.Models.ViewModels;
using Application.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Application.Web.Controllers
{
    public class BaseController : Controller
    {
        protected IUoWData Data;
        public BaseController(IUoWData data)
        {
            this.Data = data;
        }

        public BaseController()
            : this(new UoWData())
        {
            var model = new LayoutModel();
            model.Pages = this.Data.Pages
                .All()
                .OrderBy(x => x.Id)
                .Select(x => new PageViewModel
                {
                    Id = x.Id,
                    Title = x.Title
                })
                .ToList();
            //ViewBag.LayoutModel = model;

            // Populate Categories - TODO: use Cache
            model.PopularProduct = GetRandomFeaturedProduct();
            model.Categories = GetAllCategories();
            ViewBag.LayoutModel = model;
        }

        private ProductViewModel GetRandomFeaturedProduct()
        {
            return this.Data.Products
                .All()
                .Where(x => x.IsFeatured == true)
                .OrderBy(x => Guid.NewGuid())
                .ToList()
                .Select(x => new ProductViewModel
                {
                    CategoryId = x.Category.Id,
                    CategoryName = x.Category.Name,
                    DateAdded = x.DateAdded,
                    Id = x.Id,
                    Name = x.Name,
                    PrimaryImage = x.Images.First(image => image.IsPrimary),
                    ShortDescription = x.ShortDescription,
                    Slug = x.Slug
                }).FirstOrDefault();
        }

        private IEnumerable<CategoryViewModel> GetAllCategories()
        {
            return this.Data.Categories
                .All()
                .OrderBy(x => x.DisplayOrder)
                .AsEnumerable()
                .Select(x => new CategoryViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    SubCategories = this.Data.SubCategories
                        .All()
                        .OrderBy(y => y.DisplayOrder)
                        .Where(y => y.CategoryId == x.Id)
                        .Select(y => new SubCategoryViewModel
                        {
                            Id = y.Id,
                            Name = y.Name,
                            Description = y.Description
                        })
                }).ToList();

        }
    }
}
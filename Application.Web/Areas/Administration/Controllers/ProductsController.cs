using Application.Web.Areas.Administration.InputModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new CreateProductInputModel();
            model.LongDescription = "Simple Long Description from ProductsController";
            model.ShortDescription = "Simple Short Description from ProductsController";
            model.BulletsText = @"<ul>
                                <li>Тегло - 10&nbsp;кг</li>
                                <li>Широчина - 20 мм</li>
                                <li>други - 20 гр</li>
                                <li>още нещо - 500 м&nbsp;</li>
                                </ul>";
            model.Categories = GetCategories();
            model.SubCategories = GetSubCategories();
            return this.View(model);
        }
    }
}
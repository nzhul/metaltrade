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
            return this.View(model);
        }
    }
}
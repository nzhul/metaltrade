using Application.Models.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Application.Web.Controllers
{
    public class PagesController : BaseController
    {
        // GET: Pages
        [HttpGet]
        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var pageDb = this.Data.Pages.Find(id);
            if (pageDb == null)
            {
                return HttpNotFound();
            }
            var model = new Page
            {
                Id = pageDb.Id,
                Title = pageDb.Title,
                ShortDescription = pageDb.ShortDescription,
                Content = pageDb.Content
            };

            return this.View(model);
        }
    }
}
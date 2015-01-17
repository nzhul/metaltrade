using Application.Models.Articles;
using Application.Web.Areas.Administration.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Application.Web.Controllers
{
    public class ArticlesController : BaseController
    {
        [HttpGet]
        public ActionResult Index()
        {
            var model = this.Data.Articles
                .All()
                .OrderByDescending(x => x.DateCreated)
                .AsEnumerable()
                .Select(x => new ArticleViewModel 
                {
                    Id = x.Id,
                    Title = x.Title,
                    ShortDescription = x.ShortDescription
                }).ToList();
            return View(model);
        }


        // GET: Articles
        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var pageDb = this.Data.Articles.Find(id);
            if (pageDb == null)
            {
                return HttpNotFound();
            }
            var model = new Article
            {
                Id = pageDb.Id,
                Title = pageDb.Title,
                Content = pageDb.Content
            };

            return this.View(model);
        }
    }
}
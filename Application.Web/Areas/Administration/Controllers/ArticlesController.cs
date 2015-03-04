using Application.Web.Areas.Administration.Models.InputModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Application.Models.Articles;
using System.Net;
using Application.Web.Areas.Administration.Models.ViewModels;
using Utilities;

namespace Application.Web.Areas.Administration.Controllers
{
    public class ArticlesController : BaseController
    {
        const int PageSize = 10;

        // GET: Administration/Articles
        [HttpGet]
        public ActionResult Index(int? id)
        {
            int pageNumber = id.GetValueOrDefault(1);
            var articlesList = this.Data.Articles.All()
                .OrderByDescending(x => x.DateCreated)
                .Skip((pageNumber - 1) * PageSize).Take(PageSize)
                .AsEnumerable()
                .Select(x => new ArticleViewModel 
                {
                    Id = x.Id,
                    Title = x.Title,
                    ShortDescription = x.ShortDescription
                })
                .ToList();

            ViewBag.Pages = Math.Ceiling((double)this.Data.Articles.All().Count() / PageSize);
            ViewBag.CurrentPage = id;
            return View(articlesList);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateArticleInputModel article)
        {
            if (ModelState.IsValid)
            {
                var currentUserId = this.User.Identity.GetUserId();
                string slug = SlugGenerator.Generate(article.Title);
                var newArticle = new Article
                {
                    ApplicationUserId = currentUserId,
                    ShortDescription = article.ShortDescription,
                    Content = article.Content,
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now,
                    Title = article.Title,
                    Slug = slug
                };


                this.Data.Articles.Add(newArticle);
                this.Data.SaveChanges();
                TempData["message"] = "Успещно добави <strong>нова статия</strong> с заглавие: " + article.Title;
                TempData["messageType"] = "success";
                return RedirectToAction("Index");
            }

            TempData["message"] = "Невалидни данни за статията!<br/> Моля попълнете <strong>всички</strong> полета в червено!";
            TempData["messageType"] = "danger";
            return View(article);
        }


        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var articleDb = this.Data.Articles.Find(id);
            if (articleDb == null)
            {
                return HttpNotFound();
            }
            var model = new CreateArticleInputModel
            {
                Id = articleDb.Id,
                Title = articleDb.Title,
                ShortDescription = articleDb.ShortDescription,
                Content = articleDb.Content,
                Slug = articleDb.Slug
            };

            return this.View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CreateArticleInputModel article)
        {
            if (ModelState.IsValid)
            {
                var dbArticle = this.Data.Articles.Find(article.Id);
                if (dbArticle == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                dbArticle.Title = article.Title;
                dbArticle.Content = article.Content;
                dbArticle.ShortDescription = article.ShortDescription;
                dbArticle.DateModified = DateTime.Now;
                dbArticle.Slug = article.Slug;

                this.Data.SaveChanges();
                TempData["message"] = "Статията беше <strong>редактирана</strong> успешно <strong><a href='#'>ПРЕГЛЕДАЙ Я!</a></strong>";
                TempData["messageType"] = "success";
                return RedirectToAction("Index");
            }

            TempData["message"] = "Невалидни данни за статията!<br/> Моля попълнете <strong>всички</strong> полета в червено!";
            TempData["messageType"] = "danger";
            return View(article);
        }

        public ActionResult DeleteArticle(int? id)
        {

            var theArticle = this.Data.Articles.Find(id);
            if (theArticle == null)
            {
                return HttpNotFound();
            }

            this.Data.Articles.Delete(id);
            this.Data.SaveChanges();
            return Content(@"<div class='alert alert-dismissable alert-success'>
                            <button type='button' class='close' data-dismiss='alert'>×</button>
                            <strong>Статията беше изтрита успешно!</strong>
                        </div>");
        }
    }
}
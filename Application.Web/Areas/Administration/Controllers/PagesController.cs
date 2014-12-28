using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Application.Models.Pages;
using Application.Web.Areas.Administration.Models.InputModels;
using Microsoft.AspNet.Identity;
using Application.Web.Areas.Administration.Models.ViewModels;

namespace Application.Web.Areas.Administration.Controllers
{
    public class PagesController : BaseController
    {
        const int PageSize = 10;

        // GET: Administration/Articles
        [HttpGet]
        public ActionResult Index(int? id)
        {
            int pageNumber = id.GetValueOrDefault(1);
            var pagesList = this.Data.Pages.All()
                .OrderByDescending(x => x.DateCreated)
                .Skip((pageNumber - 1) * PageSize).Take(PageSize)
                .AsEnumerable()
                .Select(x => new PageViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    ShortDescription = x.ShortDescription
                })
                .ToList();

            ViewBag.Pages = Math.Ceiling((double)this.Data.Pages.All().Count() / PageSize);
            ViewBag.CurrentPage = id;
            return View(pagesList);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreatePageInputModel page)
        {
            if (ModelState.IsValid)
            {
                var currentUserId = this.User.Identity.GetUserId();
                var newPage = new Page
                {
                    ApplicationUserId = currentUserId,
                    ShortDescription = page.ShortDescription,
                    Content = page.Content,
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now,
                    Title = page.Title
                };


                this.Data.Pages.Add(newPage);
                this.Data.SaveChanges();
                TempData["message"] = "Успещно добави <strong>нова страница</strong> с заглавие: " + page.Title;
                TempData["messageType"] = "success";
                return RedirectToAction("Index");
            }

            TempData["message"] = "Невалидни данни за страницата!<br/> Моля попълнете <strong>всички</strong> полета в червено!";
            TempData["messageType"] = "danger";
            return View(page);
        }


        [HttpGet]
        public ActionResult Edit(int? id)
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
            var model = new CreatePageInputModel
            {
                Id = pageDb.Id,
                Title = pageDb.Title,
                ShortDescription = pageDb.ShortDescription,
                Content = pageDb.Content
            };

            return this.View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CreatePageInputModel page)
        {
            if (ModelState.IsValid)
            {
                var dbPage = this.Data.Pages.Find(page.Id);
                if (dbPage == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                dbPage.Title = page.Title;
                dbPage.Content = page.Content;
                dbPage.ShortDescription = page.ShortDescription;
                dbPage.DateModified = DateTime.Now;

                this.Data.SaveChanges();
                TempData["message"] = "Страницата беше <strong>редактирана</strong> успешно <strong><a href='#'>ПРЕГЛЕДАЙ Я!</a></strong>";
                TempData["messageType"] = "success";
                return RedirectToAction("Index");
            }

            TempData["message"] = "Невалидни данни за страницата!<br/> Моля попълнете <strong>всички</strong> полета в червено!";
            TempData["messageType"] = "danger";
            return View(page);
        }

        public ActionResult DeletePage(int? id)
        {

            var thePage = this.Data.Pages.Find(id);
            if (thePage == null)
            {
                return HttpNotFound();
            }

            this.Data.Pages.Delete(id);
            this.Data.SaveChanges();
            return Content(@"<div class='alert alert-dismissable alert-success'>
                            <button type='button' class='close' data-dismiss='alert'>×</button>
                            <strong>Страницата беше изтрита успешно!</strong>
                        </div>");
        }
    }
}
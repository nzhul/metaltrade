using Application.Web.Areas.Administration.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Application.Web.Areas.Administration.Controllers
{
    public class TagsController : BaseController
    {
        // GET: Administration/Tags
        public ActionResult Index()
        {
            var tagsList = this.Data.Tags.All()
                .OrderByDescending(x => x.Id)
                .AsEnumerable()
                .Select(x => new TagViewModel
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToList();
            return View(tagsList);
        }

        [HttpPost]
        public ActionResult DeleteTag(int id)
        {
            this.Data.Tags.Delete(id);
            this.Data.SaveChanges();
            return Content("<span class='label label-success'>Изтрито успешно!</span>");
        }
    }
}
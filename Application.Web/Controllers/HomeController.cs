using Application.Web.Areas.Administration.Models.ViewModels;
using Application.Web.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Web.Mvc;
using RiaLibrary.Web;

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
                    ShortDescription = x.ShortDescription,
                    Slug = x.Slug,
                    CategorySlug = x.Category.Slug
                }).ToList();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            var model = new ContactFormInputModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Contact(ContactFormInputModel contactData)
        {
            if (ModelState.IsValid)
            {
                string sender = ConfigurationManager.AppSettings["emailSender"];
                string receiver = ConfigurationManager.AppSettings["emailReceiver"];

                MailMessage mailMessage = new MailMessage(sender, receiver);
                mailMessage.IsBodyHtml = true;
                mailMessage.Subject = "Запитване (контактна форма): ";
                mailMessage.Body = "Имена: " + contactData.Name + "<br/>" +
                                   "Email: " + contactData.Email + "<br/>" +
                                   "Телефон: " + contactData.Phone + "<br/><br/>" +
                                   "Запитване: <br/>" + contactData.Content;

                SmtpClient smtpClient = new SmtpClient();

                // The settings are in web.config file
                smtpClient.Send(mailMessage);

                return Content(@"<div class='alert alert-dismissable alert-success'>
                            <button type='button' class='close' data-dismiss='alert'>×</button>
                            <strong>Съобщението беше изпратено успешно!</strong>
                        </div>");
            }
            else
            {
                return View("Contact", contactData);
            }
        }
    }
}
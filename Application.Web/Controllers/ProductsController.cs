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
using System.Net.Mail;
using System.Configuration;

namespace Application.Web.Controllers
{
    public class ProductsController : BaseController
    {

        // Autocomplete
        public JsonResult GetAutocomplete(string term)
        {

            var products = this.Data.Products
                .All()
                .OrderBy(x => x.Id)
                .Where(x => x.Name.Contains(term) || x.Tags.Any(tag => tag.Name.Contains(term)))
                .Select(x => new ProductAutocompleteViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    value = x.Name,
                    Description = x.ShortDescription,
                    IconUrl = x.Images.FirstOrDefault(image => image.IsPrimary).ImagePath + "_detailsSmallThumb.jpg"
                }).ToList();

            return Json(products, JsonRequestBehavior.AllowGet);
        }

        // GET: Products
        public ActionResult Index(int? category, int? subCategory, int? page)
        {
            var model = new ProductsViewModel();
            model.Products = GetProducts(category, subCategory, page, null);
            return View(model);
        }

        public ActionResult Tag(int? id)
        {
            var model = new ProductsViewModel();
            model.Products = GetProducts(null, null, null, id);
            return View("Index",model);
        }

        private IEnumerable<ProductViewModel> GetProducts(int? categoryId, int? subCategoryId, int? page, int? tagId)
        {
            var productsQuerable = this.Data.Products
                .All()
                .OrderBy(x => x.DateAdded)
                .Where(x => x.IsActive == true)
                .AsQueryable();

            if (categoryId != null)
            {
                productsQuerable = productsQuerable.Where(x => x.Category.Id == categoryId);
            }
            if (subCategoryId != null)
            {
                productsQuerable = productsQuerable.Where(x => x.SubCategories.Any(subcategory => subcategory.Id == subCategoryId));
            }
            if (tagId != null)
            {
                productsQuerable = productsQuerable.Where(x => x.Tags.Any(tag => tag.Id == tagId));
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
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = this.Data.Products.Find(id);
            if (product.IsActive == false)
            {
                product = null;
            }
            if (product == null)
            {
                return HttpNotFound();
            }

            var productViewData = new ProductDetailsViewModel
            {
                TheProduct = product,
                SimilarProducts = this.Data.Products
                .All()
                .OrderBy(x => x.DateAdded)
                .Where(x => x.Category.Id == product.Category.Id && x.Id != product.Id)
                .Take(3)
                .Select(x => new ProductViewModel
                {
                    CategoryId = x.Category.Id,
                    CategoryName = x.Category.Name,
                    DateAdded = x.DateAdded,
                    Id = x.Id,
                    Name = x.Name,
                    PrimaryImage = x.Images.FirstOrDefault(image => image.IsPrimary),
                    ShortDescription = x.ShortDescription
                }).ToList()
            };

            return View(productViewData);
        }

        [HttpPost]
        public ActionResult SendRequestEmail(string fullName, 
            string email, string company, string phone, string requestText,
            int width, int weight, int productId)
        {
            string sender = ConfigurationManager.AppSettings["emailSender"];
            string receiver = ConfigurationManager.AppSettings["emailReceiver"];
            var theProduct = this.Data.Products.Find(productId);

            MailMessage mailMessage = new MailMessage(sender, receiver);
            mailMessage.Subject = "Запитване за продукт: " + theProduct.Name;
            mailMessage.Body = "Имена: " + fullName + "<br/>" +
                               "Email: " + email + "<br/>" +
                               "Фирма: " + company + "<br/>" +
                               "Телефон: " + phone + "<br/><br/>" +
                               "Запитване: <br/>" + requestText;

            SmtpClient smtpClient = new SmtpClient();

            // The settings are in web.config file
            smtpClient.Send(mailMessage);

            return RedirectToAction("Index");
            
        }
    }
}

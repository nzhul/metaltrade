using Application.Web.Areas.Administration.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Application.Web.Models
{
    public class ProductsViewModel
    {
        private IEnumerable<ProductViewModel> products;
        private IEnumerable<ArticleViewModel> articles;
        public ProductsViewModel()
        {
            this.products = new List<ProductViewModel>();
            this.articles = new List<ArticleViewModel>();
        }


        public IEnumerable<ArticleViewModel> Articles
        {
            get { return this.articles; }
            set { this.articles = value; }
        }

        public IEnumerable<ProductViewModel> Products
        {
            get { return this.products; }
            set { this.products = value; }
        }
    }
}
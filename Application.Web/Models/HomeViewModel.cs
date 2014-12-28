using Application.Web.Areas.Administration.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Application.Web.Models
{
    public class HomeViewModel
    {
        private IEnumerable<PageViewModel> pages;
        private IEnumerable<ProductViewModel> products;
        private IEnumerable<CategoryViewModel> categories;
        public HomeViewModel()
        {
            this.pages = new List<PageViewModel>();
            this.products = new List<ProductViewModel>();
            this.categories = new List<CategoryViewModel>();
        }

        public IEnumerable<PageViewModel> Pages
        {
            get { return this.pages; }
            set { this.pages = value; }
        }

        public IEnumerable<ProductViewModel> Products
        {
            get { return this.products; }
            set { this.products = value; }
        }

        public IEnumerable<CategoryViewModel> Categories
        {
            get { return this.categories; }
            set { this.categories = value; }
        }
    }
}
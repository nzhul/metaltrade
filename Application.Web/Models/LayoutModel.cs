using Application.Web.Areas.Administration.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Application.Web.Models
{
    public class LayoutModel
    {
        private IEnumerable<PageViewModel> pages;
        private IEnumerable<CategoryViewModel> categories;
        private ProductViewModel popularProduct;
        public LayoutModel()
        {
            this.pages = new List<PageViewModel>();
            this.categories = new List<CategoryViewModel>();
            this.popularProduct = new ProductViewModel();
        }

        public ProductViewModel PopularProduct 
        {
            get { return this.popularProduct; }
            set { this.popularProduct = value; } 
        }

        public IEnumerable<PageViewModel> Pages
        {
            get { return this.pages; }
            set { this.pages = value; }
        }

        public IEnumerable<CategoryViewModel> Categories
        {
            get { return this.categories; }
            set { this.categories = value; }
        }
    }
}
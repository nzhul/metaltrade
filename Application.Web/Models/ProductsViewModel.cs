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
        public ProductsViewModel()
        {
            this.products = new List<ProductViewModel>();
        }


        public IEnumerable<ProductViewModel> Products
        {
            get { return this.products; }
            set { this.products = value; }
        }
    }
}
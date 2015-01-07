using Application.Models;
using Application.Web.Areas.Administration.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Application.Web.Models
{
    public class ProductDetailsViewModel
    {
        public Product TheProduct { get; set; }
        public IEnumerable<ProductViewModel> SimilarProducts { get; set; }
    }
}
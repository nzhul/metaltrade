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
        public LayoutModel()
        {
            this.pages = new List<PageViewModel>();
            this.categories = new List<CategoryViewModel>();
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Application.Web.Areas.Administration.Models.ViewModels
{
    public class CategoryViewModel
    {
        private IEnumerable<SubCategoryViewModel> subCategories;
        public CategoryViewModel()
        {
            this.subCategories = new List<SubCategoryViewModel>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int LoopCounter { get; set; }

        public string Description { get; set; }

        public IEnumerable<SubCategoryViewModel> SubCategories
        {
            get { return this.subCategories; }
            set { this.subCategories = value; }
        }
    }
}
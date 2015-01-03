using Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Application.Web.Models
{
    public class ProductDetailsViewModel
    {
        private ICollection<Tag> tags;
        private ICollection<Image> images;
        public ProductDetailsViewModel()
        {
            this.tags = new HashSet<Tag>();
            this.images = new HashSet<Image>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public string BulletsText { get; set; }
        public bool IsFeatured { get; set; }
        public DateTime DateAdded { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<Image> Images
        {
            get { return this.images; }
            set { this.images = value; }
        }
        public virtual ICollection<Tag> Tags
        {
            get { return this.tags; }
            set { this.tags = value; }
        }
    }
}
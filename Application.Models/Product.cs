using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class Product
    {
        private ICollection<Tag> tags;
        private ICollection<Image> images;
        private ICollection<SubCategory> subCategories;
        public Product()
        {
            this.tags = new HashSet<Tag>();
            this.images = new HashSet<Image>();
            this.subCategories = new HashSet<SubCategory>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public string BulletsText { get; set; }
        public bool IsFeatured { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateAdded { get; set; }
        public int DisplayOrder { get; set; }

        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<SubCategory> SubCategories
        {
            get { return this.subCategories; }
            set { this.subCategories = value; }
        }

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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class Product
    {
        private ICollection<Tag> tags;
        private ICollection<Image> images;
        public Product()
        {
            this.tags = new HashSet<Tag>();
            this.images = new HashSet<Image>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public string BulletsText { get; set; }
        public bool IsFeatured { get; set; }

        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public int SubCategoryId { get; set; }
        public virtual SubCategory SubCategory { get; set; }


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

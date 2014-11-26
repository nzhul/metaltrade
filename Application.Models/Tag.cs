using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Application.Models
{
    public class Tag
    {
        private ICollection<Product> products;
        public Tag()
        {
            this.products = new HashSet<Product>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public virtual ICollection<Product> Products
        { 
            get { return this.products; } 
            set { this.products = value; } 
        }
    }
}

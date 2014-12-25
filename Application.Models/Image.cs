using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string ImagePath { get; set; }
        public string ImageExtension { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public bool IsPrimary { get; set; }
    }
}

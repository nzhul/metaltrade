using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Application.Web.Areas.Administration.Models.InputModels
{
    public class SubCategoryInputModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Slug { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public string Description { get; set; }

        public int DisplayOrder { get; set; }
    }
}
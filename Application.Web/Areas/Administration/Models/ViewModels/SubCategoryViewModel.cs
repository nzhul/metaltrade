﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Web.Areas.Administration.Models.ViewModels
{
    public class SubCategoryViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Slug { get; set; }

        public int CategoryId { get; set; }

        public string Description { get; set; }

        public int DisplayOrder { get; set; }
    }
}

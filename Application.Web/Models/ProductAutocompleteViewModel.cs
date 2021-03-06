﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Application.Web.Models
{
    public class ProductAutocompleteViewModel
    {
        public int Id { get; set; }
        public string value { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public string IconUrl { get; set; }
    }
}
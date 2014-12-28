using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Application.Web.Areas.Administration.Models.ViewModels
{
    public class PageViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public int LoopCounter { get; set; }
    }
}
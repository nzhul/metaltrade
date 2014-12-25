using Application.Web.Areas.Administration.Models.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Application.Web.Areas.Administration.Models.InputModels
{
    public class UploadPhotoModel
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }

        //[FileSize(10240)]
        //[FileTypes("jpg,jpeg,png")]
        public IEnumerable<HttpPostedFileBase> Files { get; set; }
    }
}
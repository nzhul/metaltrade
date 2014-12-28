using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Application.Web.Areas.Administration.Models.InputModels
{
    public class CreatePageInputModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Заглавието на страницата е задължително!")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Невалидно заглавие - Максимална дължина 50 символа, минимална 3")]
        [Display(Name = "Заглавие на страницата:")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Съдържанието на страницата е задължително!")]
        [AllowHtml]
        [Display(Name = "Съдържание:")]
        [DataType("tinymce_full")]
        [UIHint("tinymce_full")]
        public string Content { get; set; }

        [Required(ErrorMessage = "Краткото описание на страницата е задължително!")]
        [StringLength(500, MinimumLength = 3, ErrorMessage = "Максимална дължина 500 символа, минимална 3")]
        [AllowHtml]
        [Display(Name = "Кратко описание:")]
        [DataType("tinymce_full")]
        [UIHint("tinymce_full")]
        public string ShortDescription { get; set; }
    }
}
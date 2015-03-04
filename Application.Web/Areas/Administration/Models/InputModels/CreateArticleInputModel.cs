using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Application.Web.Areas.Administration.Models.InputModels
{
    public class CreateArticleInputModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Заглавието на статията е задължително!")]
        [StringLength(250, MinimumLength = 3, ErrorMessage = "Невалидно заглавие - Максимална дължина 250 символа, минимална 3")]
        [Display(Name = "Заглавие на статията:")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Слъг-а е задължителен! Без него линка към сайта няма да работи!")]
        [StringLength(250, MinimumLength = 3, ErrorMessage = "Невалиден слъг - Максимална дължина 80 символа, минимална 3")]
        [Display(Name = "Slug:")]
        public string Slug { get; set; }

        [Required(ErrorMessage = "Съдържанието на статията е задължително!")]
        [AllowHtml]
        [Display(Name = "Съдържание:")]
        [DataType("tinymce_full")]
        [UIHint("tinymce_full")]
        public string Content { get; set; }

        [Required(ErrorMessage = "Краткото описание на статията е задължително!")]
        [StringLength(500, MinimumLength = 3, ErrorMessage = "Максимална дължина 500 символа, минимална 3")]
        [AllowHtml]
        [Display(Name = "Кратко описание:")]
        [DataType("tinymce_full")]
        [UIHint("tinymce_full")]
        public string ShortDescription { get; set; }
    }
}
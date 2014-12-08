using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Application.Web.Areas.Administration.InputModels
{
    public class CreateProductInputModel
    {
        [Required]
        [Display(Name = "Наименование на продукта:")]
        public string Title { get; set; }

        [Required]
        [AllowHtml]
        [Display(Name = "Кратко описание:")]
        [DataType("tinymce_full")]
        [UIHint("tinymce_full")]
        public string ShortDescription { get; set; }

        [Required]
        [AllowHtml]
        [Display(Name = "Дълго описание:")]
        [DataType("tinymce_full")]
        [UIHint("tinymce_full")]
        public string LongDescription { get; set; }

        [Required]
        [AllowHtml]
        [Display(Name = "Булети:")]
        [DataType("tinymce_full")]
        [UIHint("tinymce_full")]
        public string BulletsText { get; set; }

        // TODO: Create custon validation for the tags
        [Required]
        [Display(Name = "Тагове: ")]
        public string Tags { get; set; }

        [Display(Name = "Категория")]
        public int SelectedCategoryId { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }

        [Display(Name = "Подкатегория")]
        public int SelectedSubCategoryId { get; set; }
        public IEnumerable<SelectListItem> SubCategories { get; set; }

        [Display(Name = "На Заглавна страница: ")]
        public bool IsFeatured { get; set; }

        [Display(Name = "Пибличен: ")]
        public bool IsActive { get; set; }
    }
}
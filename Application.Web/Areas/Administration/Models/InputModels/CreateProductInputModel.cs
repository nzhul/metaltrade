using Application.Models;
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
        public int Id { get; set; }

        [Required(ErrorMessage="Името на продукта е задължително!")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Невалидно име - Максимална дължина 50 символа, минимална 3")]
        [Display(Name = "Наименование на продукта:")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Краткото описание е задължително!")]
        [AllowHtml]
        [Display(Name = "Кратко описание:")]
        [DataType("tinymce_full")]
        [UIHint("tinymce_full")]
        public string ShortDescription { get; set; }

        [Required(ErrorMessage = "Дългото описание е задължително!")]
        [AllowHtml]
        [Display(Name = "Дълго описание:")]
        [DataType("tinymce_full")]
        [UIHint("tinymce_full")]
        public string LongDescription { get; set; }

        [Required(ErrorMessage = "Булетите са задължителни!")]
        [AllowHtml]
        [Display(Name = "Булети:")]
        [DataType("tinymce_full")]
        [UIHint("tinymce_full")]
        public string BulletsText { get; set; }

        // TODO: Create custon validation for the tags
        [Required(ErrorMessage = "Таговете са задължителни!")]
        [Display(Name = "Тагове: ")]
        public string Tags { get; set; }

        [Required(ErrorMessage="Задължително!")]
        [Display(Name = "Категория")]
        public int SelectedCategoryId { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }

        [Required(ErrorMessage = "Задължително!")]
        [Display(Name = "Подкатегория")]
        public int SelectedSubCategoryId { get; set; }
        public IEnumerable<SelectListItem> SubCategories { get; set; }

        [Display(Name = "На Заглавна страница: ")]
        public bool IsFeatured { get; set; }

        [Display(Name = "Пибличен: ")]
        public bool IsActive { get; set; }

        public IEnumerable<Image> Images { get; set; }
    }
}
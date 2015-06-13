using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Application.Web.Models
{
    public class ProductRequestInputModel
    {
        [Required(ErrorMessage = "Моля попълнете вашето собствено и фамилно име!")]
        [StringLength(250, MinimumLength = 3, ErrorMessage = "Невалидно име - Максимална дължина 250 символа, минимална 3")]
        [Display(Name = "Имена:")]
        public string Name { get; set; }

        [Display(Name = "Email:")]
        [Required(ErrorMessage = "Моля предоставете валиден email!")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Телефон:")]
        [Required(ErrorMessage = "Моля предоставете валиден телефонен номер!")]
        public string Phone { get; set; }

        [Display(Name = "Фирма:")]
        public string Company { get; set; }

        public string ProductName { get; set; }


        [Required(ErrorMessage = "Съдържанието е задължително!")]
        [StringLength(5000, MinimumLength = 3, ErrorMessage = "Невалидно заглавие - Максимална дължина 5000 символа, минимална 3")]
        [Display(Name = "Въпрос:")]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }



        [Display(Name = "Диаметър(мм):")]
        public string Width { get; set; }

        [Display(Name = "Тонаж(т):")]
        public string Weight { get; set; }

        [Display(Name = "Дължина(м):")]
        public string Length { get; set; }

        [Display(Name = "Брой:")]
        public string Count { get; set; }

        public string SpecialValue { get; set; } // Anti-Spam-Bot Field
    }
}
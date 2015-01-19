using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Application.Web.Models
{
    public class ContactFormInputModel
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


        [Required(ErrorMessage = "Заглавието е задължително!")]
        [StringLength(250, MinimumLength = 3, ErrorMessage = "Невалидно заглавие - Максимална дължина 250 символа, минимална 3")]
        [Display(Name = "Относно:")]
        public string Subject { get; set; }


        [Required(ErrorMessage = "Съдържанието е задължително!")]
        [StringLength(5000, MinimumLength = 3, ErrorMessage = "Невалидно заглавие - Максимална дължина 5000 символа, минимална 3")]
        [Display(Name = "Съдържание:")]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }
    }
}
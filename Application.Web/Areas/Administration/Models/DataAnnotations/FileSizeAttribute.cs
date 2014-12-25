using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Application.Web.Areas.Administration.Models.DataAnnotations
{
    public class FileSizeAttribute : ValidationAttribute
    {
        private readonly int _maxSize;

        public FileSizeAttribute(int maxSize)
        {
            _maxSize = maxSize;
        }

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }
            else
            {
                return (value as HttpPostedFileBase).ContentLength <= _maxSize;
            }
        }

        public override string FormatErrorMessage(string name)
        {
            return String.Format("Размера на файла не трябва да е повече от {0}", _maxSize);
        }
    }
}
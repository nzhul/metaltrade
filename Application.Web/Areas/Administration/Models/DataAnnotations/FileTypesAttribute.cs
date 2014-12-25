using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Application.Web.Areas.Administration.Models.DataAnnotations
{
    public class FileTypesAttribute : ValidationAttribute
    {
        private readonly List<string> _types;

        public FileTypesAttribute(string types)
        {
            _types = types.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
        }

        public override bool IsValid(object value)
        {
            if (value == null) return true;

            var fileExt = System.IO.Path.GetExtension((value as HttpPostedFileBase).FileName).Substring(1);
            return _types.Contains(fileExt, StringComparer.OrdinalIgnoreCase);
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format("Невалиден формат на картинката. Разрешените файлове формати са: {0}.", String.Join(", ", _types));
        }
    }
}
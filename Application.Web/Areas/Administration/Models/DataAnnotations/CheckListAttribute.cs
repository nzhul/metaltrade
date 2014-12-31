using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Application.Web.Areas.Administration.Models.DataAnnotations
{
    public class CheckListAttribute : ValidationAttribute
    {
        int _length;
        bool _fixed;

        public override bool IsValid(object value)
        {
            int l = 0;

            if (value != null && typeof(List<int>) == value.GetType())
            {
                var v = (List<int>)value;
                l = v.Count;
            }
            else if (value != null)
            {
                var v = (List<string>)value;
                l = v.Count;
            }

            if (this._fixed)
            {
                return value != null && this._length == l;
            }
            else
            {
                return value != null && l >= this._length;
            }
        }

        public CheckListAttribute(int length = 1, bool isFixed = false)
        {
            this._length = length;
            this._fixed = isFixed;
        }
    }
}
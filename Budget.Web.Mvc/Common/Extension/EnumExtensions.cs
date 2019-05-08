using Budget.Domain.Core.Entity;
using Budget.Domain.Core.Enums;
using Budget.Web.Mvc.Models.Templates;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Budget.Web.Mvc.Common.Extension
{
    public static class EnumExtensions
    {
        public static SelectList ToList<TEnum>(this TEnum enumObj)
            where TEnum : struct, IComparable, IFormattable, IConvertible
        {
            var values = from TEnum e in Enum.GetValues(typeof(TEnum))
                         select new
                         {
                             Id = e,
                             Name = (e as Enum).GetDescriptionOfEnum()
                         };
            return new SelectList(values, "Id", "Name", enumObj);
        }

        public static string GetDescriptionOfEnum(this Enum value)
        {
            var type = value.GetType();
            if (!type.IsEnum) throw new ArgumentException(String.Format("Type '{0}' is not Enum", type));

            var members = type.GetMember(value.ToString());
            if (members.Length == 0) throw new ArgumentException(String.Format("Member '{0}' not found in type '{1}'", value, type.Name));

            var member = members[0];
            var attributes = member.GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.DisplayAttribute), false);
            if (attributes.Length == 0) throw new ArgumentException(String.Format("'{0}.{1}' doesn't have DisplayAttribute", type.Name, value));

            var attribute = (System.ComponentModel.DataAnnotations.DisplayAttribute)attributes[0];
            return attribute.Description;
        }

        public static BootstrapSelectVm ToBootstrapSelectVm(this Category category)
        {
            return new BootstrapSelectVm()
            {
                SelectedItem = category.Id.ToString(),
                SelectedItemText = category.Name,
                SourceList = CategoryType.Incoming.ToList()
            };
        }

        public static BootstrapSelectVm ToBootstrapSelectVm(this CategoryType category)
        {
            return new BootstrapSelectVm()
            {
                SelectedItem = category.ToString(),
                SelectedItemText = category.GetDescriptionOfEnum(),
                SourceList = CategoryType.Incoming.ToList()
            };
        }
    }
}

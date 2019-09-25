using Shared.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace FamilyControlCenter.Helpers
{
    public static class FccEnumHelper
    {
        public static IEnumerable<SelectListItem> GetTranslatedSelectListItemCollection(Type type, string culture)
        {
            var cultureInfo = new CultureInfo(culture);

            var list = System.Web.Mvc.Html.EnumHelper.GetSelectList(type);
            return list.Select(e => new SelectListItem()
            {
                Text = Resources.Resource.ResourceManager
                    .GetString(e.Text, cultureInfo),
                Value = e.Value
            });
        }

        public static IEnumerable<SelectListItem> GetTranslatedRelationTypesAsSelectedListItemCollection(Type type, string culture)
        {
            var list = new List<SelectListItem>();

            return Enum.GetValues(type)
                .Cast<RelationType>()
                .Select(e => new SelectListItem()
                {
                    Text = Resources.Resource.ResourceManager
                        .GetString(GetEnumDescription(e), new CultureInfo(culture)),
                    Value = ((int)e).ToString()
                });
        }

        private static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes = fi.
                GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

            if (attributes != null && attributes.Any())
            {
                return attributes.First().Description;
            }

            return value.ToString();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using System.ComponentModel;
using System.Web.Mvc.Html;
using System.Web.Mvc;
using Shared.Enums;

namespace FamilyControlCenter.Helpers
{
    public static class RelationTypeHelper
    {
        public static IEnumerable<SelectListItem> GetEnumDescriptionSelectList(Type type)
        {
            var list = new List<SelectListItem>();
            return Enum.GetValues(type)
                .Cast<RelationType>()
                .Select(e => new SelectListItem() { Text = GetEnumDescription(e), Value = ((int)e).ToString() });
        }

        public static string GetEnumDescription(Enum value)
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

        public static RelationType GetRelationSynonym()
        {

            throw new NotImplementedException();
        }
    }
}
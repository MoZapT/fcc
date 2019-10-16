using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Web.Mvc;

namespace FamilyControlCenter.Helpers
{
    public static class FccEnumHelper
    {
        public static IEnumerable<SelectListItem> GetTranslatedSelectListItemCollection<T>(Type type, bool isFemaleGender = false)
        {
            var list = new List<SelectListItem>();
            return Enum.GetValues(type)
                .Cast<T>()
                .Select(e => new SelectListItem()
                {
                    Text = GetGenderizedText(GetEnumDescription(type, e.ToString()), isFemaleGender ? 1 : 0),
                    Value = e.ToString()
                })
                .ToList();
        }

        public static string GetLocalizedStringForEnumValue(this ResourceManager source, Type type, Enum value, bool isFemaleGender = false)
        {
            return GetGenderizedText(GetEnumDescription(type, value.ToString()), isFemaleGender ? 1 : 0);
        }

        public static string GetGenderizedText(string mainText, int gender)
        {
            string[] array = mainText.Split('|');

            if (array == null || array.Length <= 0)
            {
                return string.Empty;
            }

            try
            {
                return GetLocalization(array[gender]);
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        private static string GetEnumDescription(Type type, string value)
        {
            FieldInfo fi = type.GetField(value);

            DescriptionAttribute[] attributes = 
                fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

            if (attributes != null && attributes.Any())
            {
                return attributes.First().Description;
            }

            return value.ToString();
        }

        private static string GetLocalization(string localString)
        {
            var rMgr = Resources.Resource.ResourceManager;
            return rMgr.GetString(localString);
        }
    }
}
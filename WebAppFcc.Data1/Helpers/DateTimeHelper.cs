using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppFcc.Data.Helpers
{
    public static class DateTimeHelper
    {
        public static string ToString(DateTime? source, string format)
        {
            if (source == null) return string.Empty;

            return ((DateTime)source).ToString(format);
        }
    }
}

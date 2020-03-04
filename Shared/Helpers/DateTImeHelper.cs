using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Helpers
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

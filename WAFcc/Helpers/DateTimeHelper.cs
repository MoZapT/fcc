﻿using System;

namespace WAFcc.Helpers
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaRequest.WebUI.Business.Extensions
{
    public static class StringExtensions
    {
        public static string NullIfEmpty(this string s)
        {
            if (s == string.Empty)
                return null;
            else
                return s;
        }

        public static string Capitalize(this string s)
        {
            return s.Substring(0, 1).ToUpper() + s.Substring(1).ToLower();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaRequest.WebUI.Business.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime? NullIfMin(this DateTime input)
        {
            if (input == DateTime.MinValue)
                return null;
            else
                return input;
        }
    }
}

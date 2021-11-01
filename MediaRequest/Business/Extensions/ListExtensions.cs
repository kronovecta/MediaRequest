using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaRequest.Domain.Interfaces;

namespace MediaRequest.WebUI.Business.Extensions
{
    public static class ListExtensions
    {
        public static IEnumerable<T> TakeRows<T>(this IEnumerable<T> credits, int rows, int cols = 3)
        {
            return credits.Take(rows * cols).ToList();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;

namespace MediaRequest.Application.Queries.People.GetCombinedMedia
{
    public class GetCombinedMediaException : Exception
    {
        public GetCombinedMediaException(string message) : base(message)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace MediaRequest.Application.Queries.Movies
{
    public class GetCreditsException : Exception
    {
        public GetCreditsException(string message) : base(message)
        {
        }
    }
}

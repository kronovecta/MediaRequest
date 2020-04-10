using System;
using System.Collections.Generic;
using System.Text;

namespace MediaRequest.Application.Queries.Requests.GetSingleRequest
{
    public class RequestExistsResponse
    {
        public bool Accepted { get; set; }
        public bool Exists { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace MediaRequest.Application.Clients
{
    public class CustomHttpClient : ICustomHttpClient
    {
        public HttpClient Client { get; set; }
    }
}

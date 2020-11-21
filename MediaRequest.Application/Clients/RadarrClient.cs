using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace MediaRequest.Application.Clients
{
    public class RadarrClient : ICustomHttpClient
    {
        public HttpClient Client { get; }

        public RadarrClient(HttpClient httpClient)
        {
            Client = httpClient;
        }
    }
}

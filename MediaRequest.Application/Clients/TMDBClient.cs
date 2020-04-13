using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace MediaRequest.Application.Clients
{
    public class TMDBClient : ICustomHttpClient
    {
        public HttpClient Client { get; }

        public TMDBClient(HttpClient httpClient)
        {
            Client = httpClient;
        }
    }
}

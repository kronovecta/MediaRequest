using MediaRequest.Application.Parsers;
using MediaRequest.Domain.Configuration;
using MediaRequest.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MediaRequest.Application.Clients
{
    public class RadarrClient : ClientBase<IRadarrType>, ICustomHttpClient
    {
        public RadarrClient(HttpClient httpClient) : base(httpClient)
        {
        }
    }
}

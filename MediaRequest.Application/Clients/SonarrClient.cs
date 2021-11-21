using MediaRequest.Application.Parsers;
using MediaRequest.Application.Queries.Television;
using MediaRequest.Domain.Configuration;
using MediaRequest.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MediaRequest.Application.Clients
{
    public class SonarrClient : ClientBase<ISonarrType>, ICustomHttpClient
    {
        public SonarrClient(HttpClient httpClient) : base(httpClient)
        {
        }
    }
}

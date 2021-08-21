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
    public class SonarrClient : ICustomHttpClient
    {
        private readonly ApiKeys _apiKeys;

        public string AllSeries => $"api/series?apikey={_apiKeys.Sonarr}";
        public HttpClient Client { get; }

        public SonarrClient(HttpClient httpClient, IOptions<ApiKeys> apiKeys)
        {
            Client = httpClient;
            _apiKeys = apiKeys.Value;
        }

        public async Task<TResponseType> GetSonarrResponseSingle<TResponseType>(string url) 
            where TResponseType : ISonarrType, new()
        {
            var res = await Client.GetAsync(url);

            try
            {
                res.EnsureSuccessStatusCode();

                using (var stream = await res.Content.ReadAsStreamAsync())
                {
                    return await JsonSerializer.DeserializeAsync<TResponseType>(stream, DefaultJsonSettings.Settings);
                }
            }
            catch (Exception ex)
            {
                return new TResponseType();
            }
        }

        public async Task<IEnumerable<TResponseType>> GetSonarrResponseCollection<TResponseType>(string url)
            where TResponseType : ISonarrType
        {
            var res = await Client.GetAsync(url);

            try
            {
                res.EnsureSuccessStatusCode();

                using (var stream = await res.Content.ReadAsStreamAsync())
                {
                    return await JsonSerializer.DeserializeAsync<IEnumerable<TResponseType>>(stream, DefaultJsonSettings.Settings);
                    
                }
            }
            catch (Exception ex)
            {
                return new List<TResponseType>();
            }
        }
    }
}

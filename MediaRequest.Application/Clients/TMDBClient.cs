using MediaRequest.Application.Parsers;
using MediaRequest.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace MediaRequest.Application.Clients
{
    public class TMDBClient : ICustomHttpClient
    {
        public HttpClient Client { get; }

        public TMDBClient(HttpClient httpClient)
        {
            Client = httpClient;
        }

        public async Task<TResponseType> GetSingle<TResponseType>(string url)
            where TResponseType : IApolloType, new()
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

        public async Task<IEnumerable<TResponseType>> GetCollection<TResponseType>(string url)
            where TResponseType : IApolloType
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

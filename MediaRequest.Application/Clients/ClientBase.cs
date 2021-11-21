using MediaRequest.Application.Parsers;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MediaRequest.Application.Clients
{
    public abstract class ClientBase<T>
    {
        public HttpClient Client { get; }

        public ClientBase(HttpClient httpClient)
        {
            Client = httpClient;
        }


        public async Task<TResponseType> GetResponseSingle<TResponseType>(string url)
            where TResponseType : T, new()
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

        public async Task<IEnumerable<TResponseType>> GetResponseCollection<TResponseType>(string url)
            where TResponseType : T
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

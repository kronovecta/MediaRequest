using MediaRequest.Application.Clients;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace MediaRequest.Infrastructure.Notifications.Discord
{
    public class DiscordClient : ICustomHttpClient
    {
        public HttpClient Client { get => new HttpClient(); }

    }
}

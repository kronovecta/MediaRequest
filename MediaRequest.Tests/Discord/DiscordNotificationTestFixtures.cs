using MediaRequest.Application.Clients;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace MediaRequest.Tests
{
    public class DiscordNotificationTestFixtures
    {        
        public DiscordNotificationTestFixtures()
        {
            
        }
    }

    public class DiscordTestClient : ICustomHttpClient
    {
        public HttpClient Client { get => new HttpClient(); }
    }
}

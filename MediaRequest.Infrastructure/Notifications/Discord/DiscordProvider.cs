using MediaRequest.Application;
using MediaRequest.Application.Clients;
using MediaRequest.Infrastructure.Notifications;
using MediaRequest.Infrastructure.Notifications.Discord;
using System;
using System.Collections.Specialized;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MediaRequest.Infrastructure.Notifications.Discord
{
    public class DiscordProvider : NotificationBase<DiscordSettings>
    {
        public DiscordSettings Settings { get; set; }
        public ICustomHttpClient _client { get; set; }
        public DiscordProvider()
        {
            _client = new DiscordClient();
        }

        public override async void OnRequest()
        {
            await TestMessage();
        }

        public override void OnApprove()
        {
            throw new NotImplementedException();
        }

        public override void OnDownload()
        {
            throw new NotImplementedException();
        }

        public override void OnReject()
        {
            throw new NotImplementedException();
        }

        //    private readonly DiscordClient _client;
        //    private readonly WebClient webClient;
        //    private NameValueCollection values = new NameValueCollection();
        //    private readonly Encoding _encoding = new UTF8Encoding();

        //    public DiscordProvider(/*DiscordClient client*/)
        //    {
        //        //_client = client;
        //        webClient = new WebClient();

        //    }

        //    public void Dispose()
        //    {
        //        webClient.Dispose();
        //    }

        //    public async Task Send()
        //    {
        //        var message = new DiscordMessage
        //        {
        //            content = "Test message",
        //            username = "Apollo"
        //        };

        //        var uri = new Uri("https://discordapp.com/api/webhooks/699377511011713024/NK0hnJZG_QSntYIAU9hrPM3gNB6k1v0SY2WkHNLEJsDmEXjR6hJsIf96vw-LdnXzEZDZ");

        //        //HttpContent stringContent = new StringContent(System.Text.Json.JsonSerializer.Serialize<DiscordMessage>(message));

        //        using (WebClient client = new WebClient())
        //        {
        //            NameValueCollection data = new NameValueCollection();

        //            var response = client.UploadValues(uri, "POST", data);
        //            var responseText = _encoding.GetString(response);

        //            if (true)
        //            {

        //            }
        //        }
        //    }

        public override async Task<HttpResponseMessage> TestMessage()
        {
            var payload = CreatePayload("This is a test message");

            #warning INSERT API KEY
            var uri = new Uri("");

            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Post, uri);
                request.Content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");

                return await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(true);
            }
        }

        private DiscordMessage CreatePayload(string message, string username = "Apollo", string avatarUrl = "")
        {
            return new DiscordMessage
            {
                content = message,
                username = username,
                avatar_url = avatarUrl
            };
        }
    }
}

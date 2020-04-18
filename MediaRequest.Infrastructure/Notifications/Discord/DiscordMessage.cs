using System;
using System.Collections.Generic;
using System.Text;

namespace MediaRequest.Infrastructure.Notifications.Discord
{
    //public class DiscordUser
    //{
    //    public string username { get; set; }
    //    public string discriminator { get; set; }
    //    public string id { get; set; }
    //    public string avatar { get; set; }
    //}

    //public class DiscordMessage
    //{
    //    public string name { get; set; }
    //    public int type { get; set; }
    //    public string channel_id { get; set; }
    //    public string token { get; set; }
    //    public object avatar { get; set; }
    //    public string guild_id { get; set; }
    //    public string id { get; set; }
    //    public DiscordUser user { get; set; }
    //}

    public class DiscordMessage
    {
        public string content { get; set; }
        public string username { get; set; }
        public string avatar_url { get; set; }
        public bool tts { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace MediaRequest.Infrastructure.Notifications.Discord
{
    public class DiscordSettings : INotificationSettings
    {
        public string WebhookUrl { get; set; }
        public string Username { get; set; }
        public string Avatar { get; set; }
    }
}

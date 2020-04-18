using MediaRequest.Infrastructure.Notifications.Discord;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediaRequest.Infrastructure.Notifications
{
    public class Providers
    {
        private readonly DiscordProvider _discord;

        public Providers(DiscordProvider discord)
        {
            _discord = discord;
        }

        public DiscordProvider Discord { get => _discord; }
    }
}

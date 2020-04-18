using MediaRequest.Application;
using MediaRequest.Infrastructure.Notifications;
using MediaRequest.Infrastructure.Notifications.Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaRequest.WebUI.Models.IdentityModels
{
    public class ApplicationUserWrapper
    {
        public ApplicationUser User { get; set; }
        private readonly IMediaDbContext _context;
        public DiscordProvider _discordProvider { get => new DiscordProvider(); }

        public IEnumerable<INotificationBase> NotificationProviders { get => GetProviders(); }

        public ApplicationUserWrapper(IMediaDbContext context, ApplicationUser user)
        {
            _context = context;
            User = user;
        }

        private IEnumerable<INotificationBase> GetProviders()
        {
            var providers = _context.NotificationProvider.Where(x => x.UserId == User.Id);

            foreach (var provider in providers)
            {
                switch (provider.ProviderType.ToUpper())
                {
                    case "DISCORD":
                        yield return GetDiscordProvider;
                        break;
                    default:
                        break;
                }

            }
        }

        private DiscordProvider GetDiscordProvider
        {
            get
            {
                var providerSettings = _context.NotificationProvider.SingleOrDefault(x => x.ProviderType.ToUpper() == "DISCORD");

                if (providerSettings != null)
                {
                    var settings = new DiscordSettings { Avatar = providerSettings.AvatarUrl, Username = providerSettings.Username, WebhookUrl = providerSettings.WebhookURL };
                    _discordProvider.Settings = settings;

                    return _discordProvider;
                }

                return null;
            }
        }
    }
}

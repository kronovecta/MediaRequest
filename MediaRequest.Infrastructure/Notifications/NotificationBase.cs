using MediaRequest.Application.Clients;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MediaRequest.Infrastructure.Notifications
{
    public abstract class NotificationBase<TSettings> : INotificationBase where TSettings : INotificationSettings
    {
        public ICustomHttpClient Client { get; set; }

        public virtual void OnApprove()
        {
        }

        public virtual void OnDownload()
        {
        }

        public virtual void OnReject()
        {
        }

        public virtual void OnRequest()
        {
        }

        public virtual Task<HttpResponseMessage> TestMessage()
        {
            throw new NotImplementedException();
        }
    }
}

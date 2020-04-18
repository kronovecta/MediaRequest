using MediaRequest.Application;
using MediaRequest.Application.Clients;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace MediaRequest.Infrastructure.Notifications
{
    public interface INotificationBase
    {
        public ICustomHttpClient Client { get; set; }
        void OnRequest();
        void OnApprove();
        void OnReject();
        void OnDownload();
    }
}

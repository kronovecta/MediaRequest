using System;
using System.Collections.Generic;
using System.Text;

namespace MediaRequest.Domain.Configuration
{
    public class ServicePath
    {

        public string Radarr { get; set; }
        public string TMDB { get; set; }
        public string BaseURL
        {
            get
            {
                var uri = new Uri(Radarr);
                return uri.Scheme + "://" + uri.Authority + "/";
            }
        }
    }
}

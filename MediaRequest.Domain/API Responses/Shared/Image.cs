using MediaRequest.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace MediaRequest.Domain.API_Responses.Shared
{
    public class Image
    {
        [JsonPropertyName("coverType")]
        public string CoverType { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("remoteUrl")]
        public string RemoteUrl { get; set; }
    }
}

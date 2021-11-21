using MediaRequest.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace MediaRequest.Domain.API_Responses.Sonarr
{
    public class Season : ISonarrType
    {
        [JsonPropertyName("seasonNumber")]
        public int SeasonNumber { get; set; }

        [JsonPropertyName("monitored")]
        public bool Monitored { get; set; }

        [JsonPropertyName("statistics")]
        public Statistics Statistics { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace MediaRequest.Domain.API_Responses.Shared
{
    public class OuterQuality
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("source")]
        public string Source { get; set; }

        [JsonPropertyName("resolution")]
        public int Resolution { get; set; }

        [JsonPropertyName("quality")]
        public Quality Quality { get; set; }

        [JsonPropertyName("revision")]
        public Revision Revision { get; set; }
    }

    public class Quality
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("source")]
        public string Source { get; set; }

        [JsonPropertyName("resolution")]
        public int Resolution { get; set; }
    }
}

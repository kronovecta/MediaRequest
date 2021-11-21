using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace MediaRequest.Domain.API_Responses.Radarr.v3
{
    public class Collection
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("tmdbId")]
        public int TmdbId { get; set; }

        [JsonPropertyName("images")]
        public List<object> Images { get; set; }
    }
}

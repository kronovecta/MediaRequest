using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace MediaRequest.Domain.API_Responses.TvMaze
{
    public class Image
    {
        [JsonPropertyName("medium")]
        public string Medium { get; set; }

        [JsonPropertyName("original")]
        public string Original { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace MediaRequest.Domain.API_Responses.Shared
{
    public class Rating
    {
        [JsonPropertyName("votes")]
        public int Votes { get; set; }

        [JsonPropertyName("value")]
        public double Value { get; set; }
    }
}

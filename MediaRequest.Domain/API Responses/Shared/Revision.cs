using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace MediaRequest.Domain.API_Responses.Shared
{
    public class Revision
    {
        [JsonPropertyName("version")]
        public int Version { get; set; }

        [JsonPropertyName("real")]
        public int Real { get; set; }

        [JsonPropertyName("isRepack")]
        public bool IsRepack { get; set; }
    }
}

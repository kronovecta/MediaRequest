using MediaRequest.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace MediaRequest.Domain.API_Responses.TvMaze
{
    public class Self
    {
        [JsonPropertyName("href")]
        public string Href { get; set; }
    }

    public class Links
    {
        [JsonPropertyName("self")]
        public Self Self { get; set; }
    }

    public class CastMember : IApolloType
    {
        [JsonPropertyName("person")]
        public Person Person { get; set; }

        [JsonPropertyName("character")]
        public Character Character { get; set; }

        [JsonPropertyName("self")]
        public bool Self { get; set; }

        [JsonPropertyName("voice")]
        public bool Voice { get; set; }
    }
}

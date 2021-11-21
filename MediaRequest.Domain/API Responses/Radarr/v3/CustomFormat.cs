using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace MediaRequest.Domain.API_Responses.Radarr.v3
{
    public class CustomFormat
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("includeCustomFormatWhenRenaming")]
        public bool IncludeCustomFormatWhenRenaming { get; set; }

        [JsonPropertyName("specifications")]
        public List<Specification> Specifications { get; set; }

        public class Specification
        {
            [JsonPropertyName("name")]
            public string Name { get; set; }

            [JsonPropertyName("implementation")]
            public string Implementation { get; set; }

            [JsonPropertyName("implementationName")]
            public string ImplementationName { get; set; }

            [JsonPropertyName("infoLink")]
            public string InfoLink { get; set; }

            [JsonPropertyName("negate")]
            public bool Negate { get; set; }

            [JsonPropertyName("required")]
            public bool Required { get; set; }

            [JsonPropertyName("fields")]
            public List<Field> Fields { get; set; }

            public class Field
            {
                [JsonPropertyName("order")]
                public int Order { get; set; }

                [JsonPropertyName("name")]
                public string Name { get; set; }

                [JsonPropertyName("label")]
                public string Label { get; set; }

                [JsonPropertyName("value")]
                public string Value { get; set; }

                [JsonPropertyName("type")]
                public string Type { get; set; }

                [JsonPropertyName("advanced")]
                public bool Advanced { get; set; }
            }
        }
    }
}

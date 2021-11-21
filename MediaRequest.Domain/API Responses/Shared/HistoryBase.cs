using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace MediaRequest.Domain.API_Responses.Shared
{
    public class HistoryBase
    {

        [JsonPropertyName("date")]
        public DateTime Date { get; set; }

        [JsonPropertyName("eventType")]
        public string EventType { get; set; }
    }
}

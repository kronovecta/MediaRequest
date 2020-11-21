using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace MediaRequest.Application.Parsers
{
    public static class DefaultJsonSettings
    {
        public static JsonSerializerOptions Settings {
            get => new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            };
        }
    }
}

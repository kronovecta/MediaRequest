﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace MediaRequest.Domain.API_Responses.Shared
{
    public class MediaBase
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("sortTitle")]
        public string SortTitle { get; set; }

        [JsonPropertyName("overview")]
        public string Overview { get; set; }

        [JsonPropertyName("images")]
        public List<Image> Images { get; set; }

        [JsonPropertyName("path")]
        public string Path { get; set; }

        [JsonIgnore]
        public string EncodedPath => "ftp://bertil:MLPMnNqDMMrFa8r2@bass.seedhost.eu:14039" + Path;

        [JsonIgnore]
        public string PosterUrl
        {
            get
            {
                var image = Images.SingleOrDefault(x => x.CoverType == "poster");
                return image?.RemoteUrl ?? image?.Url;
            }
        }

        [JsonIgnore]
        public string FanartUrl
        {
            get
            {
                var image = Images.SingleOrDefault(x => x.CoverType == "fanart");
                return image?.RemoteUrl ?? image?.Url;
            }
        }
    }
}

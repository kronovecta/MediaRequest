using Microsoft.FeatureManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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

        [JsonPropertyName("added")]
        public DateTime Added { get; set; }

        [JsonIgnore]
        public string EncodedPath => Path;

        [JsonIgnore]
        public virtual string PosterUrl
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

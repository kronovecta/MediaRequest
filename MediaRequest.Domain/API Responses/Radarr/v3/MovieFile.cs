using MediaRequest.Domain.API_Responses.Shared;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace MediaRequest.Domain.API_Responses.Radarr.v3
{
    public class MovieFile
    {
        [JsonPropertyName("movieId")]
        public int MovieId { get; set; }

        [JsonPropertyName("relativePath")]
        public string RelativePath { get; set; }

        [JsonPropertyName("path")]
        public string Path { get; set; }

        [JsonPropertyName("size")]
        public object Size { get; set; }

        [JsonPropertyName("dateAdded")]
        public DateTime DateAdded { get; set; }

        [JsonPropertyName("indexerFlags")]
        public int IndexerFlags { get; set; }

        [JsonPropertyName("quality")]
        public OuterQuality Quality { get; set; }

        [JsonPropertyName("mediaInfo")]
        public MediaInfo MediaInfo { get; set; }

        [JsonPropertyName("qualityCutoffNotMet")]
        public bool QualityCutoffNotMet { get; set; }

        [JsonPropertyName("languages")]
        public List<Language> Languages { get; set; }

        [JsonPropertyName("edition")]
        public string Edition { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("sceneName")]
        public string SceneName { get; set; }

        [JsonPropertyName("releaseGroup")]
        public string ReleaseGroup { get; set; }
    }
}

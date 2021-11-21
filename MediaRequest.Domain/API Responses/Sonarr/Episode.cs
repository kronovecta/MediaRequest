using MediaRequest.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace MediaRequest.Domain.API_Responses.Sonarr
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);


    public class Episode : ISonarrType
    {
        [JsonPropertyName("seriesId")]
        public int SeriesId { get; set; }

        [JsonPropertyName("episodeFileId")]
        public int EpisodeFileId { get; set; }

        [JsonPropertyName("seasonNumber")]
        public int SeasonNumber { get; set; }

        [JsonPropertyName("episodeNumber")]
        public int EpisodeNumber { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("airDate")]
        public string AirDate { get; set; }

        [JsonPropertyName("airDateUtc")]
        public DateTime AirDateUtc { get; set; }

        [JsonPropertyName("hasFile")]
        public bool HasFile { get; set; }

        [JsonPropertyName("monitored")]
        public bool Monitored { get; set; }

        [JsonPropertyName("unverifiedSceneNumbering")]
        public bool UnverifiedSceneNumbering { get; set; }

        [JsonPropertyName("series")]
        public Series Series { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }
    }


}

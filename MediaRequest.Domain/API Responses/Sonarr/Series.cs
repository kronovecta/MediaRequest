using MediaRequest.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace MediaRequest.Domain.API_Responses.Sonarr
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
    public class AlternateTitle
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("seasonNumber")]
        public int SeasonNumber { get; set; }

        [JsonPropertyName("sceneSeasonNumber")]
        public int? SceneSeasonNumber { get; set; }
    }

    public class Statistics
    {
        [JsonPropertyName("episodeFileCount")]
        public int EpisodeFileCount { get; set; }

        [JsonPropertyName("episodeCount")]
        public int EpisodeCount { get; set; }

        [JsonPropertyName("totalEpisodeCount")]
        public int TotalEpisodeCount { get; set; }

        [JsonPropertyName("sizeOnDisk")]
        public object SizeOnDisk { get; set; }

        [JsonPropertyName("percentOfEpisodes")]
        public double PercentOfEpisodes { get; set; }

        [JsonPropertyName("previousAiring")]
        public DateTime? PreviousAiring { get; set; }

        [JsonPropertyName("nextAiring")]
        public DateTime? NextAiring { get; set; }
    }



    public class Series : ISonarrType
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("alternateTitles")]
        public List<AlternateTitle> AlternateTitles { get; set; }

        [JsonPropertyName("sortTitle")]
        public string SortTitle { get; set; }

        [JsonPropertyName("seasonCount")]
        public int SeasonCount { get; set; }

        [JsonPropertyName("totalEpisodeCount")]
        public int TotalEpisodeCount { get; set; }

        [JsonPropertyName("episodeCount")]
        public int EpisodeCount { get; set; }

        [JsonPropertyName("episodeFileCount")]
        public int EpisodeFileCount { get; set; }

        [JsonPropertyName("sizeOnDisk")]
        public object SizeOnDisk { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("overview")]
        public string Overview { get; set; }

        [JsonPropertyName("nextAiring")]
        public DateTime NextAiring { get; set; }

        [JsonPropertyName("previousAiring")]
        public DateTime PreviousAiring { get; set; }

        [JsonPropertyName("network")]
        public string Network { get; set; }

        [JsonPropertyName("airTime")]
        public string AirTime { get; set; }

        [JsonPropertyName("images")]
        public List<Image> Images { get; set; }

        [JsonPropertyName("seasons")]
        public List<Season> Seasons { get; set; }

        [JsonPropertyName("year")]
        public int Year { get; set; }

        [JsonPropertyName("path")]
        public string Path { get; set; }

        [JsonPropertyName("profileId")]
        public int ProfileId { get; set; }

        [JsonPropertyName("languageProfileId")]
        public int LanguageProfileId { get; set; }

        [JsonPropertyName("seasonFolder")]
        public bool SeasonFolder { get; set; }

        [JsonPropertyName("monitored")]
        public bool Monitored { get; set; }

        [JsonPropertyName("useSceneNumbering")]
        public bool UseSceneNumbering { get; set; }

        [JsonPropertyName("runtime")]
        public int Runtime { get; set; }

        [JsonPropertyName("tvdbId")]
        public int TvdbId { get; set; }

        [JsonPropertyName("tvRageId")]
        public int TvRageId { get; set; }

        [JsonPropertyName("tvMazeId")]
        public int TvMazeId { get; set; }

        [JsonPropertyName("firstAired")]
        public DateTime FirstAired { get; set; }

        [JsonPropertyName("lastInfoSync")]
        public DateTime LastInfoSync { get; set; }

        [JsonPropertyName("seriesType")]
        public string SeriesType { get; set; }

        [JsonPropertyName("cleanTitle")]
        public string CleanTitle { get; set; }

        [JsonPropertyName("imdbId")]
        public string ImdbId { get; set; }

        [JsonPropertyName("titleSlug")]
        public string TitleSlug { get; set; }

        [JsonPropertyName("certification")]
        public string Certification { get; set; }

        [JsonPropertyName("genres")]
        public List<string> Genres { get; set; }

        [JsonPropertyName("tags")]
        public List<object> Tags { get; set; }

        [JsonPropertyName("added")]
        public DateTime Added { get; set; }

        [JsonPropertyName("ratings")]
        public Rating Ratings { get; set; }

        [JsonPropertyName("qualityProfileId")]
        public int QualityProfileId { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }
    }
}
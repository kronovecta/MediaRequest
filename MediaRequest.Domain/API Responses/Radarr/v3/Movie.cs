using MediaRequest.Domain.API_Responses.Shared;
using MediaRequest.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;

namespace MediaRequest.Domain.API_Responses.Radarr.v3
{
    public class Movie : IRadarrType
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("originalTitle")]
        public string OriginalTitle { get; set; }

        [JsonPropertyName("alternateTitles")]
        public List<AlternateTitle> AlternateTitles { get; set; }

        [JsonPropertyName("secondaryYearSourceId")]
        public int SecondaryYearSourceId { get; set; }

        [JsonPropertyName("sortTitle")]
        public string SortTitle { get; set; }

        [JsonPropertyName("sizeOnDisk")]
        public object SizeOnDisk { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("overview")]
        public string Overview { get; set; }

        [JsonPropertyName("inCinemas")]
        public DateTime InCinemas { get; set; }

        [JsonPropertyName("physicalRelease")]
        public DateTime PhysicalRelease { get; set; }

        [JsonPropertyName("digitalRelease")]
        public DateTime DigitalRelease { get; set; }

        [JsonPropertyName("images")]
        public List<Image> Images { get; set; }

        [JsonPropertyName("website")]
        public string Website { get; set; }

        [JsonPropertyName("year")]
        public int Year { get; set; }

        [JsonPropertyName("hasFile")]
        public bool HasFile { get; set; }

        [JsonPropertyName("youTubeTrailerId")]
        public string YouTubeTrailerId { get; set; }

        [JsonPropertyName("studio")]
        public string Studio { get; set; }

        [JsonPropertyName("path")]
        public string Path { get; set; }

        [JsonPropertyName("qualityProfileId")]
        public int QualityProfileId { get; set; }

        [JsonPropertyName("monitored")]
        public bool Monitored { get; set; }

        [JsonPropertyName("minimumAvailability")]
        public string MinimumAvailability { get; set; }

        [JsonPropertyName("isAvailable")]
        public bool IsAvailable { get; set; }

        [JsonPropertyName("folderName")]
        public string FolderName { get; set; }

        [JsonPropertyName("runtime")]
        public int Runtime { get; set; }

        [JsonPropertyName("cleanTitle")]
        public string CleanTitle { get; set; }

        [JsonPropertyName("imdbId")]
        public string ImdbId { get; set; }

        [JsonPropertyName("tmdbId")]
        public int TmdbId { get; set; }

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

        [JsonPropertyName("movieFile")]
        public MovieFile MovieFile { get; set; }

        [JsonPropertyName("collection")]
        public Collection Collection { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("secondaryYear")]
        public int? SecondaryYear { get; set; }

        public string PosterUrl => Images.SingleOrDefault(x => x.CoverType == "poster")?.RemoteUrl;
        public string FanartUrl => Images.SingleOrDefault(x => x.CoverType == "fanart")?.RemoteUrl;
    }
}

using MediaRequest.Domain.API_Responses.Shared;
using MediaRequest.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace MediaRequest.Domain.API_Responses.Sonarr
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);

    public class Data
    {
        [JsonPropertyName("fileId")]
        public string FileId { get; set; }

        [JsonPropertyName("droppedPath")]
        public string DroppedPath { get; set; }

        [JsonPropertyName("importedPath")]
        public string ImportedPath { get; set; }

        [JsonPropertyName("downloadClient")]
        public string DownloadClient { get; set; }

        [JsonPropertyName("downloadClientName")]
        public string DownloadClientName { get; set; }

        [JsonPropertyName("preferredWordScore")]
        public string PreferredWordScore { get; set; }
    }

    public class Record
    {
        [JsonPropertyName("episodeId")]
        public int EpisodeId { get; set; }

        [JsonPropertyName("seriesId")]
        public int SeriesId { get; set; }

        [JsonPropertyName("sourceTitle")]
        public string SourceTitle { get; set; }

        [JsonPropertyName("quality")]
        public OuterQuality Quality { get; set; }

        [JsonPropertyName("qualityCutoffNotMet")]
        public bool QualityCutoffNotMet { get; set; }

        [JsonPropertyName("date")]
        public DateTime Date { get; set; }

        [JsonPropertyName("downloadId")]
        public string DownloadId { get; set; }

        [JsonPropertyName("eventType")]
        public string EventType { get; set; }

        [JsonPropertyName("data")]
        public Data Data { get; set; }

        [JsonPropertyName("episode")]
        public Episode Episode { get; set; }

        [JsonPropertyName("series")]
        public Series Series { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }
    }

    public class History : ISonarrType
    {
        [JsonPropertyName("page")]
        public int Page { get; set; }

        [JsonPropertyName("pageSize")]
        public int PageSize { get; set; }

        [JsonPropertyName("sortKey")]
        public string SortKey { get; set; }

        [JsonPropertyName("sortDirection")]
        public string SortDirection { get; set; }

        [JsonPropertyName("totalRecords")]
        public int TotalRecords { get; set; }

        [JsonPropertyName("records")]
        public List<Record> Records { get; set; }
    }
}

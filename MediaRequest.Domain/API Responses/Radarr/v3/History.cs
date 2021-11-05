using MediaRequest.Domain.API_Responses.Shared;
using MediaRequest.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace MediaRequest.Domain.API_Responses.Radarr.v3
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
    
    public class Revision
    {
        [JsonPropertyName("version")]
        public int Version { get; set; }

        [JsonPropertyName("real")]
        public int Real { get; set; }

        [JsonPropertyName("isRepack")]
        public bool IsRepack { get; set; }
    }

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

        [JsonPropertyName("reason")]
        public string Reason { get; set; }

        [JsonPropertyName("indexer")]
        public string Indexer { get; set; }

        [JsonPropertyName("nzbInfoUrl")]
        public string NzbInfoUrl { get; set; }

        [JsonPropertyName("releaseGroup")]
        public string ReleaseGroup { get; set; }

        [JsonPropertyName("age")]
        public string Age { get; set; }

        [JsonPropertyName("ageHours")]
        public string AgeHours { get; set; }

        [JsonPropertyName("ageMinutes")]
        public string AgeMinutes { get; set; }

        [JsonPropertyName("publishedDate")]
        public DateTime? PublishedDate { get; set; }

        [JsonPropertyName("size")]
        public string Size { get; set; }

        [JsonPropertyName("downloadUrl")]
        public string DownloadUrl { get; set; }

        [JsonPropertyName("guid")]
        public string Guid { get; set; }

        [JsonPropertyName("tvdbId")]
        public string TvdbId { get; set; }

        [JsonPropertyName("tvRageId")]
        public string TvRageId { get; set; }

        [JsonPropertyName("protocol")]
        public string Protocol { get; set; }

        [JsonPropertyName("indexerFlags")]
        public string IndexerFlags { get; set; }

        [JsonPropertyName("indexerId")]
        public string IndexerId { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }
    }

    public class Record : HistoryBase
    {
        [JsonPropertyName("movieId")]
        public int MovieId { get; set; }

        [JsonPropertyName("sourceTitle")]
        public string SourceTitle { get; set; }

        [JsonPropertyName("languages")]
        public List<Language> Languages { get; set; }

        [JsonPropertyName("quality")]
        public OuterQuality Quality { get; set; }

        [JsonPropertyName("customFormats")]
        public List<CustomFormat> CustomFormats { get; set; }

        [JsonPropertyName("qualityCutoffNotMet")]
        public bool QualityCutoffNotMet { get; set; }

        [JsonPropertyName("downloadId")]
        public string DownloadId { get; set; }

        [JsonPropertyName("data")]
        public Data Data { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }
    }

    public class History : IRadarrType
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

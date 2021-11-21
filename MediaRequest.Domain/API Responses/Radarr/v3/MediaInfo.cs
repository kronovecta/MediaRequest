using MediaRequest.Domain.API_Responses.Shared;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace MediaRequest.Domain.API_Responses.Radarr.v3
{
    public class MediaInfo
    {
        [JsonPropertyName("audioAdditionalFeatures")]
        public string AudioAdditionalFeatures { get; set; }

        [JsonPropertyName("audioBitrate")]
        public int AudioBitrate { get; set; }

        [JsonPropertyName("audioChannels")]
        public double AudioChannels { get; set; }

        [JsonPropertyName("audioCodec")]
        public string AudioCodec { get; set; }

        [JsonPropertyName("audioLanguages")]
        public string AudioLanguages { get; set; }

        [JsonPropertyName("audioStreamCount")]
        public int AudioStreamCount { get; set; }

        [JsonPropertyName("videoBitDepth")]
        public int VideoBitDepth { get; set; }

        [JsonPropertyName("videoBitrate")]
        public int VideoBitrate { get; set; }

        [JsonPropertyName("videoCodec")]
        public string VideoCodec { get; set; }

        [JsonPropertyName("videoFps")]
        public double VideoFps { get; set; }

        [JsonPropertyName("resolution")]
        public string Resolution { get; set; }

        [JsonPropertyName("runTime")]
        public string RunTime { get; set; }

        [JsonPropertyName("scanType")]
        public string ScanType { get; set; }

        [JsonPropertyName("subtitles")]
        public string Subtitles { get; set; }
    }


}

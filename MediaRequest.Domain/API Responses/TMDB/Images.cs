using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace MediaRequest.Domain.API_Responses.TMDB
{
    public class Backdrop : TMDBMediaBase
    {
    }

    public class Poster : TMDBMediaBase
    {
    }

    public class Images
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("backdrops")]
        public List<Backdrop> Backdrops { get; set; }

        [JsonPropertyName("posters")]
        public List<Poster> Posters { get; set; }
    }

    public abstract class TMDBMediaBase
    {
        [JsonPropertyName("aspect_ratio")]
        public double AspectRatio { get; set; }

        [JsonPropertyName("file_path")]
        public string FilePath { get; set; }

        [JsonPropertyName("height")]
        public long Height { get; set; }

        //[JsonPropertyName("iso_639_1")]
        public string iso_639_1 { get; set; }

        [JsonPropertyName("vote_average")]
        public double VoteAverage { get; set; }

        [JsonPropertyName("vote_count")]
        public long VoteCount { get; set; }

        [JsonPropertyName("width")]
        public long Width { get; set; }
    }

}

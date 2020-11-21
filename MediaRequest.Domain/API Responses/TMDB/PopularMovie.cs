using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace MediaRequest.Domain.API_Responses.TMDB
{
    public class PopularMovie
    {
        
        [JsonPropertyName("popularity")]
        public double Popularity { get; set; }
        
        [JsonPropertyName("vote_count")]
        public int VoteCount { get; set; }
        
        [JsonPropertyName("video")]
        public bool Video { get; set; }
        
        [JsonPropertyName("poster_path")]
        public string PosterPath { get; set; }
        
        [JsonPropertyName("id")]
        public int Id { get; set; }
        
        [JsonPropertyName("adult")]
        public bool Adult { get; set; }
        
        [JsonPropertyName("backdrop_path")]
        public string BackdropPath { get; set; }
        
        [JsonPropertyName("original_language")]
        public string OriginalLanguage { get; set; }
        
        [JsonPropertyName("original_title")]
        public string OriginalTitle { get; set; }
        
        [JsonPropertyName("genre_ids")]
        public List<int> GenreIds { get; set; }
        
        [JsonPropertyName("title")]
        public string Title { get; set; }
        
        [JsonPropertyName("vote_average")]
        public double VoteAverage { get; set; }
        
        [JsonPropertyName("overview")]
        public string Overview { get; set; }

        [JsonPropertyName("release_date")]
        public string ReleaseDate { get; set; }

        // Generates a slug
        [JsonIgnore]
        public string Slug
        {
            get
            {
                return string.Format("{0}-{1}", Title.ToLower().Replace(" ", "-"), Id);
            }
        }
    }

    public class PopularMovies
    {
        public int page { get; set; }
        public int total_results { get; set; }
        public int total_pages { get; set; }
        public List<PopularMovie> results { get; set; }
    }
}

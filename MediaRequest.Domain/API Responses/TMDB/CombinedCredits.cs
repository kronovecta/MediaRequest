using MediaRequest.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace MediaRequest.Domain.API_Responses.TMDB
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
    public class Cast
    {
        public string ItemName => Title ?? OriginalTitle ?? Name ?? string.Empty;

        [JsonPropertyName("adult")]
        public bool Adult { get; set; }

        [JsonPropertyName("backdrop_path")]
        public string BackdropPath { get; set; }

        [JsonPropertyName("genre_ids")]
        public List<int> GenreIds { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("original_language")]
        public string OriginalLanguage { get; set; }

        [JsonPropertyName("original_title")]
        public string OriginalTitle { get; set; }

        [JsonPropertyName("overview")]
        public string Overview { get; set; }

        [JsonPropertyName("poster_path")]
        public string PosterPath { get; set; }

        [JsonPropertyName("release_date")]
        public string ReleaseDate { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("video")]
        public bool Video { get; set; }

        [JsonPropertyName("vote_average")]
        public double VoteAverage { get; set; }

        [JsonPropertyName("vote_count")]
        public int VoteCount { get; set; }

        [JsonPropertyName("popularity")]
        public double Popularity { get; set; }

        [JsonPropertyName("character")]
        public string Character { get; set; }

        [JsonPropertyName("credit_id")]
        public string CreditId { get; set; }

        [JsonPropertyName("order")]
        public int Order { get; set; }

        [JsonPropertyName("media_type")]
        public string MediaType { get; set; }
        
        [JsonIgnore]
        public string Poster => "https://image.tmdb.org/t/p/w400" + PosterPath;

        [JsonIgnore]
        public string Backdrop => "https://image.tmdb.org/t/p/w400" + BackdropPath;

        // Generates a slug
        [JsonIgnore]
        public string Slug => string.Format("{0}-{1}", ItemName.ToLower().Replace(" ", "-"), Id);
    }

    public class Crew
    {
        [JsonPropertyName("adult")]
        public bool Adult { get; set; }

        [JsonPropertyName("backdrop_path")]
        public string BackdropPath { get; set; }

        [JsonPropertyName("genre_ids")]
        public List<int> GenreIds { get; set; }

        [JsonPropertyName("original_language")]
        public string OriginalLanguage { get; set; }

        [JsonPropertyName("original_title")]
        public string OriginalTitle { get; set; }

        [JsonPropertyName("poster_path")]
        public string PosterPath { get; set; }

        [JsonPropertyName("vote_count")]
        public int VoteCount { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("vote_average")]
        public double VoteAverage { get; set; }

        [JsonPropertyName("video")]
        public bool Video { get; set; }

        [JsonPropertyName("overview")]
        public string Overview { get; set; }

        [JsonPropertyName("release_date")]
        public string ReleaseDate { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("popularity")]
        public double Popularity { get; set; }

        [JsonPropertyName("credit_id")]
        public string CreditId { get; set; }

        [JsonPropertyName("department")]
        public string Department { get; set; }

        [JsonPropertyName("job")]
        public string Job { get; set; }

        [JsonPropertyName("media_type")]
        public string MediaType { get; set; }

        [JsonPropertyName("original_name")]
        public string OriginalName { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("first_air_date")]
        public string FirstAirDate { get; set; }

        [JsonPropertyName("origin_country")]
        public List<string> OriginCountry { get; set; }

        [JsonPropertyName("episode_count")]
        public int? EpisodeCount { get; set; }
    }

    public class CombinedCredits : IApolloType
    {
        [JsonPropertyName("cast")]
        public List<Cast> Cast { get; set; }

        [JsonPropertyName("crew")]
        public List<Crew> Crew { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }
    }


}

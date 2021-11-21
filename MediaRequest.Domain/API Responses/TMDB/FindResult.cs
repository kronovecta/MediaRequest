using MediaRequest.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace MediaRequest.Domain.API_Responses.TMDB
{
    public class FindResult : IApolloType
    {
        [JsonPropertyName("movie_results")]
        public IEnumerable<Movie> MovieResults { get; set; }

        //[JsonPropertyName("person_results")]
        //public List<object> PersonResults { get; set; }

        [JsonPropertyName("tv_results")]
        public IEnumerable<Series> TvResults { get; set; }

        [JsonPropertyName("tv_episode_results")]
        public IEnumerable<SeriesEpisode> TvEpisodeResults { get; set; }

        //[JsonPropertyName("tv_season_results")]
        //public List<object> TvSeasonResults { get; set; }
    }


}

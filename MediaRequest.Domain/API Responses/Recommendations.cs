using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace MediaRequest.Domain.API_Responses
{
    public class Recommendation
    {
        public int Id { get; set; }
        public bool Video { get; set; }
        public int Vote_count { get; set; }
        public double Vote_average { get; set; }
        public string Title { get; set; }
        public string Release_date { get; set; }
        public string Original_language { get; set; }
        public string Original_title { get; set; }
        public List<int> Genre_ids { get; set; }
        public string Backdrop_path { get; set; }
        public bool Adult { get; set; }
        public string Overview { get; set; }
        public string Poster_path { get; set; }
        public double Popularity { get; set; }
        public string PosterUrl { get; set; }
        public string FanartUrl { get; set; }

        [JsonIgnore]
        public string Slug
        {
            get
            {
                var stripped = Regex.Replace(Title.ToLower(), "[^A-Za-z0-9 ]+", "-", RegexOptions.Compiled);
                return string.Format("{0}-{1}", stripped, Id);
            }
        }
    }

    public class Recommendations
    {
        public string TmdbId { get; set; }
        public int Page { get; set; }
        public List<Recommendation> Results { get; set; }
        public int Total_pages { get; set; }
        public int Total_results { get; set; }
    }
}

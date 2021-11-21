using MediaRequest.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace MediaRequest.Domain.API_Responses.TvMaze
{
    public class Show
    {
        [JsonPropertyName("href")]
        public string Href { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("language")]
        public string Language { get; set; }

        [JsonPropertyName("genres")]
        public List<string> Genres { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("runtime")]
        public int? Runtime { get; set; }

        [JsonPropertyName("averageRuntime")]
        public int? AverageRuntime { get; set; }

        [JsonPropertyName("premiered")]
        public string Premiered { get; set; }

        [JsonPropertyName("officialSite")]
        public string OfficialSite { get; set; }

        [JsonPropertyName("schedule")]
        public Schedule Schedule { get; set; }

        [JsonPropertyName("rating")]
        public Rating Rating { get; set; }

        [JsonPropertyName("weight")]
        public int Weight { get; set; }

        [JsonPropertyName("network")]
        public object Network { get; set; }

        [JsonPropertyName("webChannel")]
        public WebChannel WebChannel { get; set; }

        [JsonPropertyName("dvdCountry")]
        public object DvdCountry { get; set; }

        [JsonPropertyName("externals")]
        public Externals Externals { get; set; }

        [JsonPropertyName("image")]
        public Image Image { get; set; }

        [JsonPropertyName("summary")]
        public string Summary { get; set; }

        [JsonPropertyName("updated")]
        public int Updated { get; set; }

        [JsonPropertyName("_links")]
        public Links Links { get; set; }
    }

    public class Schedule
    {
        [JsonPropertyName("time")]
        public string Time { get; set; }

        [JsonPropertyName("days")]
        public List<string> Days { get; set; }
    }

    public class Rating
    {
        [JsonPropertyName("average")]
        public double? Average { get; set; }
    }

    public class WebChannel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("country")]
        public Country Country { get; set; }
    }

    public class Externals
    {
        [JsonPropertyName("tvrage")]
        public object Tvrage { get; set; }

        [JsonPropertyName("thetvdb")]
        public int? Thetvdb { get; set; }

        [JsonPropertyName("imdb")]
        public string Imdb { get; set; }
    }

    public class Previousepisode
    {
        [JsonPropertyName("href")]
        public string Href { get; set; }
    }

    public class EmbeddedShow
    {
        [JsonPropertyName("show")]
        public Show Show { get; set; }
    }

    public class PersonCredit : IApolloType
    {
        [JsonPropertyName("self")]
        public bool Self { get; set; }

        [JsonPropertyName("voice")]
        public bool Voice { get; set; }

        [JsonPropertyName("_links")]
        public Links Links { get; set; }

        [JsonPropertyName("_embedded")]
        public EmbeddedShow Embedded { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace MediaRequest.Domain.API_Responses
{
    public class Cast
    {
        public string character { get; set; }
        public string credit_id { get; set; }
        public string release_date { get; set; }
        public int vote_count { get; set; }
        public bool video { get; set; }
        public bool adult { get; set; }
        public double vote_average { get; set; }
        public string title { get; set; }
        public List<object> genre_ids { get; set; }
        public string original_language { get; set; }
        public string original_title { get; set; }
        public double popularity { get; set; }
        public int id { get; set; }
        public string backdrop_path { get; set; }
        public string overview { get; set; }
        public string poster_path { get; set; }
    }

    public class Crew
    {
        public int id { get; set; }
        public string department { get; set; }
        public string original_language { get; set; }
        public string original_title { get; set; }
        public string job { get; set; }
        public string overview { get; set; }
        public int vote_count { get; set; }
        public bool video { get; set; }
        public string poster_path { get; set; }
        public string backdrop_path { get; set; }
        public string title { get; set; }
        public double popularity { get; set; }
        public List<int> genre_ids { get; set; }
        public double vote_average { get; set; }
        public bool adult { get; set; }
        public string release_date { get; set; }
        public string credit_id { get; set; }
    }

    public class OtherWorks
    {
        public List<Cast> cast { get; set; }
        public List<Crew> crew { get; set; }
        public int id { get; set; }
    }
}
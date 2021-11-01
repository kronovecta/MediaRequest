using MediaRequest.Domain;
using MediaRequest.Domain.API_Responses.Radarr.v3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaRequest.WebUI.ViewModels
{
    public class SearchResultViewModel
    {
        public string SearchTerm { get; set; }
        public List<MovieExists> Movies { get; set; }
        public Movie LatestMovie { get; set; }

        public SearchResultViewModel()
        {
            Movies = new List<MovieExists>();
        }
    }

    public class MovieExists
    {
        public Movie Movie { get; set; }
        public bool Exists { get; set; }
        public bool Monitored { get; set; }
        public bool Downloaded { get; set; }
    }
}

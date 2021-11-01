using MediaRequest.Domain.API_Responses.Sonarr;
using MediaRequest.Domain.API_Responses.Radarr.v3;
using MediaRequest.Domain.Radarr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Record = MediaRequest.Domain.API_Responses.Radarr.v3.Record;
using Movie = MediaRequest.Domain.API_Responses.Radarr.v3.Movie;
using History = MediaRequest.Domain.API_Responses.Sonarr.History;

namespace MediaRequest.WebUI.ViewModels
{
    public class IndexViewModel
    {
        public Movie LatestMovie { get; set; }
        public History LatestSeries { get; set; }
        public IndexListPartialViewModel PartialView { get; set; }
    }

    public class IndexListPartialViewModel
    {
        public string Term { get; set; }
        public int FilterMode { get; set; }
        public IEnumerable<Movie> Movies { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
    }
}

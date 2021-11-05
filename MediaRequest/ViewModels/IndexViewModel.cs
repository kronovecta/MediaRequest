using MediaRequest.Domain.API_Responses.Shared;
using System.Collections.Generic;
using History = MediaRequest.Domain.API_Responses.Sonarr.History;
using Movie = MediaRequest.Domain.API_Responses.Radarr.v3.Movie;

namespace MediaRequest.WebUI.ViewModels
{
    public class IndexViewModel
    {
        public MediaBase LatestMovie { get; set; }
        public History LatestSeries { get; set; }
        public IndexListPartialViewModel PartialView { get; set; }
    }

    public class IndexListPartialViewModel
    {
        public string Term { get; set; } = "";
        public int FilterMode { get; set; } = 0;
        public IEnumerable<Movie> Movies { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
    }
}

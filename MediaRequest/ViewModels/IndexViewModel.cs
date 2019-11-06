using MediaRequest.Domain.Radarr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaRequest.WebUI.ViewModels
{
    public class IndexViewModel
    {
        public Movie LatestMovie { get; set; }
        public IndexListPartialViewModel PartialView { get; set; }
    }

    public class IndexListPartialViewModel
    {
        public string Term { get; set; }
        public int FilterMode { get; set; }
        public IEnumerable<Movie> Movies { get; set; }
    }
}

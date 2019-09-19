using MediaRequest.WebUI.Models.API_Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaRequest.WebUI.ViewModels
{
    public class SearchViewModel
    {
        public string Input { get; set; }
        public List<Movie> Movies { get; set; }
        public MovieRequestObject RequestObject { get; set; }

        public SearchViewModel()
        {
            Movies = new List<Movie>();
        }
    }
}

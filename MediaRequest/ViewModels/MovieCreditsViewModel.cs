using MediaRequest.Domain.TMDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaRequest.WebUI.ViewModels
{
    public class MovieCreditsViewModel
    {
        public string TMDBId { get; set; }
        public Credits Credits { get; set; }
        public List<Cast> Cast { get; set; }
        public List<Cast> TopBilled { get; set; }

        public MovieCreditsViewModel()
        {
            TopBilled = Credits.Cast.GetRange(0, Credits.Cast.Count >= 5 ? 5 : Credits.Cast.Count);
            Cast = Credits.Cast;
        }
    }
}

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
        public IEnumerable<Cast> Cast { get; set; }
        public IEnumerable<Crew> Crew { get; set; }
        public IEnumerable<Cast> TopBilled { get; set; }

        public MovieCreditsViewModel(Credits sourceCast)
        {
            var amount = sourceCast.Cast.Count >= 5 ? 5 : Credits.Cast.Count;

            TopBilled = sourceCast.Cast.GetRange(0, amount);
            Cast = sourceCast.Cast.Skip(amount);
            Crew = sourceCast.Crew;
        }
    }
}

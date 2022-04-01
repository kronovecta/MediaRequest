using MediaRequest.Domain.TMDB;
using MediaRequest.WebUI.Business.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace MediaRequest.WebUI.ViewModels
{
    public class MovieCreditsViewModel
    {
        public string TMDBId { get; set; }
        public Credits Credits { get; set; }
        public IEnumerable<Cast> Cast { get; set; }
        public IEnumerable<Crew> Crew { get; set; }
        public IEnumerable<Cast> TopBilled { get; set; }

        public MovieCreditsViewModel()
        {
        }

        public MovieCreditsViewModel(Credits sourceCast)
        {
            var amount = sourceCast.Cast.Count >= 5 ? 5 : Credits.Cast.Count;

            TopBilled = sourceCast.Cast.GetRange(0, amount);
            Cast = sourceCast.Cast.Skip(amount).TakeRows(3);
            Crew = sourceCast.Crew.TakeRows(3);
        }
    }
}

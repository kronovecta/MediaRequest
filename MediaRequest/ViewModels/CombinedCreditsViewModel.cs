using MediaRequest.Domain.API_Responses.TMDB;
using System.Collections.Generic;
using System.Linq;

namespace MediaRequest.WebUI.ViewModels
{
    public class CombinedCreditsViewModel
    {
        private List<Cast> _cast;
        private List<Crew> _crew;

        public List<Cast> Cast
        {
            get => _cast.GroupBy(x => x.Id).Select(x => x.FirstOrDefault()).OrderByDescending(x => x.Popularity).ToList();
            set => _cast = value;
        }

        public List<Crew> Crew
        {
            get => _crew.GroupBy(x => x.Id).Select(x => x.FirstOrDefault()).OrderByDescending(x => x.Popularity).ToList();
            set => _crew = value;
        }
    }
}
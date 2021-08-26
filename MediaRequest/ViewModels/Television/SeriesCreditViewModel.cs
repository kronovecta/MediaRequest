using MediaRequest.Domain.API_Responses.TvMaze;
using MediaRequest.Domain.TMDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaRequest.WebUI.ViewModels.Television
{
    public class SeriesCreditViewModel
    {
        private IEnumerable<Cast> _cast;
        private IEnumerable<Cast> _topBilled;

        public SeriesCreditViewModel(IEnumerable<Cast> sourceCast)
        {
            _topBilled = sourceCast.ToList().GetRange(0, sourceCast.Count() > 5 ? 5 : sourceCast.Count());
            _cast = sourceCast.Skip(5);
        }

        public int TvMazeId { get; set; }
        public IEnumerable<Cast> Cast => _cast;
        public IEnumerable<Cast> TopBilled => _topBilled;
    }
}

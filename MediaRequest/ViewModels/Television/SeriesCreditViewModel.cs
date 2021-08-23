using MediaRequest.Domain.API_Responses.TvMaze;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaRequest.WebUI.ViewModels.Television
{
    public class SeriesCreditViewModel
    {
        private IEnumerable<CastMember> _cast;
        private IEnumerable<CastMember> _topBilled;

        public SeriesCreditViewModel(IEnumerable<CastMember> sourceCast)
        {
            _topBilled = sourceCast.ToList().GetRange(0, sourceCast.Count() > 5 ? 5 : sourceCast.Count());
            _cast = sourceCast.Skip(5);
        }

        public int TvMazeId { get; set; }
        public IEnumerable<CastMember> Cast => _cast;
        public IEnumerable<CastMember> TopBilled => _topBilled;
    }
}

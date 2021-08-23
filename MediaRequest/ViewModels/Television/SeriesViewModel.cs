using MediaRequest.Domain.API_Responses.TvMaze;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Series = MediaRequest.Domain.API_Responses.Sonarr.Series;

namespace MediaRequest.WebUI.ViewModels.Television
{
    public class SeriesViewModel
    {
        public bool Requested { get; set; }
        public bool Accepted { get; set; }
        public bool AlreadyAdded => Series.Added == DateTime.MinValue ? false : true;

        public Series Series { get; set; }
        public SeriesCreditViewModel Cast { get; set; }
    }
}

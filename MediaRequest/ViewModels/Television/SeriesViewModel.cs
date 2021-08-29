using MediaRequest.Domain.API_Responses.TvMaze;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SonarrSeries = MediaRequest.Domain.API_Responses.Sonarr.Series;
using TMDBSeries = MediaRequest.Domain.API_Responses.TMDB.Series;

namespace MediaRequest.WebUI.ViewModels.Television
{
    public class SeriesViewModel
    {
        public bool Requested { get; set; }
        public bool Accepted { get; set; }
        public bool AlreadyAdded => SonarrSeries.Added == DateTime.MinValue ? false : true;

        public SonarrSeries SonarrSeries { get; set; }
        public TMDBSeries TmdbSeries { get; set; }
        public SeriesCreditViewModel Cast { get; set; }
    }
}

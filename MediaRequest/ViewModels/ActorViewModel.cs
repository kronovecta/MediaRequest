using MediaRequest.Domain.API_Responses;
using MediaRequest.Domain.API_Responses.TMDB;
using MediaRequest.Domain.Interfaces;
using MediaRequest.Domain.Radarr;
using MediaRequest.Domain.TMDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Movie = MediaRequest.Domain.API_Responses.TMDB.Movie;

namespace MediaRequest.WebUI.ViewModels
{
    public class ActorViewModel
    {
        public Actor Actor { get; set; }
        public IEnumerable<Movie> PopularMovies { get; set; }
        public IEnumerable<IMediaType> PreviousWorks { get; set; }
    }
}

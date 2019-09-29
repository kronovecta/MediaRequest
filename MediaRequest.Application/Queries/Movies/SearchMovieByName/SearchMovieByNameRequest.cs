using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediaRequest.Application.Queries.Movies.SearchMovieByName
{
    public class SearchMovieByNameRequest : IRequest<SearchMovieByNameResponse>
    {
        public string ApiKey_Radarr { get; set; }
        public string ApiKey_TMDB { get; set; }
        public string SearchTerm { get; set; }
    }
}

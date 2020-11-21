using MediaRequest.Domain.Configuration;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediaRequest.Application.Queries.Movies.SearchMovieByName
{
    public class SearchMovieByNameRequest : IRequest<SearchMovieByNameResponse>
    {
        public ApiKeys Keys { get; set; }
        public string SearchTerm { get; set; }
    }
}

using MediaRequest.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediaRequest.Application.Queries.People.GetPopularMovies
{
    public class GetPopularMoviesRequest : IRequest<GetPopularMoviesResponse>
    {
        public MediaType MediaType { get; set; }
        public string ActorId { get; set; }
    }
}

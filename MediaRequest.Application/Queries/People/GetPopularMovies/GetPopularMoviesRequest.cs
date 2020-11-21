using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediaRequest.Application.Queries.People.GetPopularMovies
{
    public class GetPopularMoviesRequest : IRequest<GetPopularMoviesResponse>
    {
        public string ActorId { get; set; }
    }
}

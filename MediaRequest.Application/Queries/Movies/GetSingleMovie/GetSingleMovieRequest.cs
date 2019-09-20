using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediaRequest.Application.Queries
{
    public class GetSingleMovieRequest : IRequest<GetSingleMovieResponse>
    {
        public string TmdbId { get; set; }
    }
}

using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediaRequest.Application.Queries.Movies.GetSingleExistingMovie
{
    public class GetSingleExistingMovieRequest : IRequest<GetSingleExistingMovieResponse>
    {
        public string RadarrMovieId { get; set; }
    }
}

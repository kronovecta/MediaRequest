using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediaRequest.Application.Queries.Movies
{
    public class GetMovieMediaRequest : IRequest<GetMovieMediaResponse>
    {
        public string TMDBId { get; set; }
        public string ApiKey { get; set; }
    }
}

using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediaRequest.Application.Queries.Movies
{
    public class GetMovieMediaRequest : IRequest<GetMovieMediaResponse>
    {
        public GetMovieMediaRequest()
        {
        }

        public GetMovieMediaRequest(string tmdbId)
        {
            TMDBId = tmdbId;
        }

        public string TMDBId { get; set; }
    }
}

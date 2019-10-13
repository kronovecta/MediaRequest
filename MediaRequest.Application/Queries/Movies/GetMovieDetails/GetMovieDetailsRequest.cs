using MediaRequest.Domain.Configuration;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediaRequest.Application.Queries.Movies
{
    public class GetMovieDetailsRequest : IRequest<GetMovieDetailsResponse>
    {
        public string TMDBid { get; set; }
    }
}

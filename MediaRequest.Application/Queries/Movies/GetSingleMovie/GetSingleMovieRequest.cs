using MediaRequest.Domain.Configuration;
using MediatR;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediaRequest.Application.Queries
{
    public class GetSingleMovieRequest : IRequest<GetSingleMovieResponse>
    {
        public string ApiKey { get; set; }
        public string TmdbId { get; set; }
    }
}

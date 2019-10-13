using MediaRequest.Domain.TMDB;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MediaRequest.Application.Queries.Movies.GetMovieDetails
{
    public class GetMovieDetailsHandler : IRequestHandler<GetMovieDetailsRequest, GetMovieDetailsResponse>
    {
        private readonly IHttpHelper _http;

        public GetMovieDetailsHandler(IHttpHelper http)
        {
            _http = http;
        }

        public async Task<GetMovieDetailsResponse> Handle(GetMovieDetailsRequest request, CancellationToken cancellationToken)
        {

            throw new NotImplementedException();
        }
    }
}

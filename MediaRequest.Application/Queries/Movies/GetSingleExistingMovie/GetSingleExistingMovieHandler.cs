using MediaRequest.Domain.Configuration;
using MediaRequest.Domain.Radarr;
using MediatR;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MediaRequest.Application.Queries.Movies.GetSingleExistingMovie
{
    public class GetSingleExistingMovieHandler : IRequestHandler<GetSingleExistingMovieRequest, GetSingleExistingMovieResponse>
    {
        private readonly IMediaDbContext _context;
        private readonly ApiKeys _keys;
        private readonly ServicePath _path;
        private readonly IHttpHelper _http;

        public GetSingleExistingMovieHandler(IHttpHelper http, IMediaDbContext context, IOptions<ServicePath> path, IOptions<ApiKeys> keys)
        {
            _context = context;
            _keys = keys.Value;
            _path = path.Value;
            _http = http;
        }

        public async Task<GetSingleExistingMovieResponse> Handle(GetSingleExistingMovieRequest request, CancellationToken cancellationToken)
        {
            var res = await _http.GetMovie(request);
            res.EnsureSuccessStatusCode();

            var result = await res.Content.ReadAsStringAsync();
            var movie = JsonConvert.DeserializeObject<Movie>(result);

            var response = new GetSingleExistingMovieResponse
            {
                Movie = movie
            };

            return response;
        }
    }
}

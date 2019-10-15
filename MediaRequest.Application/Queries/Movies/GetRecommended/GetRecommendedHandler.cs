using MediaRequest.Domain.API_Responses;
using MediaRequest.Domain.Configuration;
using MediatR;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MediaRequest.Application.Queries.Movies
{
    public class GetRecommendedHandler : IRequestHandler<GetRecommendedRequest, GetRecommendedResponse>
    {
        private readonly IHttpHelper _http;
        private readonly ServicePath _path;

        public GetRecommendedHandler(IHttpHelper http, IOptions<ServicePath> path)
        {
            _path = path.Value;
            _http = http;
        }

        public async Task<GetRecommendedResponse> Handle(GetRecommendedRequest request, CancellationToken cancellationToken)
        {
            var res = await _http.GetRecommended(request);
            res.EnsureSuccessStatusCode();

            var result = await res.Content.ReadAsStringAsync();
            var recommendations = JsonConvert.DeserializeObject<Recommendations>(result);

            foreach (var movie in recommendations.Results)
            {
                movie.PosterUrl = "https://image.tmdb.org/t/p/w500" + movie.Poster_path;

                //movie.PosterUrl = _path.Radarr + movie.Poster_path.Split(new string[] { "/radarr" }, StringSplitOptions.None)[1];
                //movie.FanartUrl = _path.Radarr + movie.Backdrop_path.Split(new string[] { "/radarr" }, StringSplitOptions.None)[1];
            }

            var response = new GetRecommendedResponse { Recommendations = recommendations };

            return response;
        }
    }
}

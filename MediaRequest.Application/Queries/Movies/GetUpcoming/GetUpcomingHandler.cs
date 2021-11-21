using MediaRequest.Domain.Configuration;
using MediaRequest.Domain.API_Responses.Radarr.v3;
using MediatR;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace MediaRequest.Application.Queries.Movies.GetUpcoming
{
    public class GetUpcomingHandler : IRequestHandler<GetUpcomingRequest, GetUpcomingResponse>
    {
        private readonly IHttpHelper _http;
        private readonly ServicePath _path;

        public GetUpcomingHandler(IHttpHelper http, IOptions<ServicePath> path)
        {
            _http = http;
            _path = path.Value;
        }

        public async Task<GetUpcomingResponse> Handle(GetUpcomingRequest request, CancellationToken cancellationToken)
        {
            var res = await _http.GetUpcoming(request.Days);
            var response = new GetUpcomingResponse();

            if (res.IsSuccessStatusCode)
            {
                var result = await res.Content.ReadAsStringAsync();
                var movies = JsonConvert.DeserializeObject<IEnumerable<Movie>>(result);

                //movies.ToList().ForEach(x => x.PosterUrl = _path.BaseURL + x.Images.SingleOrDefault(y => y.CoverType == "poster").URL);

                response.Movies = movies;
                return response;
            } else
            {
                response.Movies = new List<Movie>();
                return response;
            }
        }
    }
}

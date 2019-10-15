using MediaRequest.Domain.Radarr;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MediaRequest.Application.Queries.Movies.GetUpcoming
{
    public class GetUpcomingHandler : IRequestHandler<GetUpcomingRequest, GetUpcomingResponse>
    {
        private readonly IHttpHelper _http;
        public GetUpcomingHandler(IHttpHelper http)
        {
            _http = http;
        }

        public async Task<GetUpcomingResponse> Handle(GetUpcomingRequest request, CancellationToken cancellationToken)
        {
            var res = await _http.GetUpcoming(request.Days);
            var response = new GetUpcomingResponse();

            if (res.IsSuccessStatusCode)
            {
                var result = await res.Content.ReadAsStringAsync();
                var movies = JsonConvert.DeserializeObject<IEnumerable<Movie>>(result);

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

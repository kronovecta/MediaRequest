using MediaRequest.Domain.TMDB;
using MediatR;
using System.Text.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediaRequest.Application.Parsers;

namespace MediaRequest.Application.Queries.Movies
{
    public class GetCreditsHandler : IRequestHandler<GetCreditsRequest, GetCreditsResponse>
    {
        private readonly IHttpHelper _http;

        public GetCreditsHandler(IHttpHelper http)
        {
            _http = http;
        }

        public async Task<GetCreditsResponse> Handle(GetCreditsRequest request, CancellationToken cancellationToken)
        {
            var response = await _http.GetCast(request);
            if (response.IsSuccessStatusCode)
            {
                var model = new GetCreditsResponse();

                using (var stream = await response.Content.ReadAsStreamAsync())
                {
                    model.Credits = await JsonSerializer.DeserializeAsync<Credits>(stream, DefaultJsonSettings.Settings);
                }

                model.Credits.Crew = model.Credits.Crew.Take(20).ToList();

                //if (request.Amount > 0)
                //{
                //    model.Credits.Cast = model.Credits.Cast.Skip(5).ToList();
                //} else
                //{
                //    model.Credits.Cast = model.Credits.Cast.Skip(5).Take(9).ToList();
                //}

                return new GetCreditsResponse
                {
                    Credits = model.Credits
                };
            } else
            {
                throw new GetCreditsException("Error retrieving credits");
            }
        }
    }
}

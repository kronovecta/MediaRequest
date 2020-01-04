using MediaRequest.Domain.TMDB;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
            using (var client = new HttpClient())
            {
                var response = await _http.GetCast(request);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var credits = JsonConvert.DeserializeObject<Credits>(result);

                    credits.Cast.ForEach(x => x.Profile_path = "https://image.tmdb.org/t/p/w200" + x.Profile_path);

                    credits.Crew = credits.Crew.Take(20).ToList();
                    credits.Crew.ForEach(x => x.Profile_path = "https://image.tmdb.org/t/p/w200" + x.Profile_path);
                    
                    credits.TopBilled = credits.Cast.Take(5).ToList();

                    if (request.Amount > 0)
                    {
                        credits.Cast = credits.Cast.Skip(5).ToList();
                    } else
                    {
                        credits.Cast = credits.Cast.Skip(5).Take(9).ToList();
                    }

                    return new GetCreditsResponse
                    {
                        Credits = credits
                        
                    };
                } else
                {
                    throw new GetCreditsException("Error retrieving credits");
                }
            }
        }
    }
}

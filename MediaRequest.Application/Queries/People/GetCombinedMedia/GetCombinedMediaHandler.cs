﻿using MediaRequest.Domain.API_Responses;
using MediaRequest.Domain.Configuration;
using MediaRequest.Domain.TMDB;
using MediatR;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Linq;

namespace MediaRequest.Application.Queries.People.GetCombinedMedia
{
    public class GetCombinedMediaHandler : IRequestHandler<GetCombinedMediaRequest, GetCombinedMediaResponse>
    {
        private readonly IHttpHelper _http;
        private readonly ApiKeys _keys;
        private readonly ServicePath _path;

        public GetCombinedMediaHandler(IHttpHelper http, IOptions<ServicePath> path, IOptions<ApiKeys> keys)
        {
            _http = http;
            _keys = keys.Value;
            _path = path.Value;
        }
        public async Task<GetCombinedMediaResponse> Handle(GetCombinedMediaRequest request, CancellationToken cancellationToken)
        {
            var actorRes = await _http.GetDetails(request.ActorID);

            if(actorRes.IsSuccessStatusCode)
            {                
                var actorResult = await actorRes.Content.ReadAsStringAsync();
                var actor = JsonConvert.DeserializeObject<Actor>(actorResult);

                actor.profile_path = "https://image.tmdb.org/t/p/w600_and_h900_bestv2/" + actor.profile_path;

                var response = new GetCombinedMediaResponse { Actor = actor };

                return response;
            } 
            else
            {
                throw new GetCombinedMediaException("Error fetching related movies");
            }
        }
    }
}

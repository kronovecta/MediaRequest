using MediaRequest.Application.Clients;
using MediaRequest.Domain.API_Responses.TvMaze;
using MediaRequest.Domain.Configuration;
using MediaRequest.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MediaRequest.Application.Queries.Television.TvMaze
{
    public class GetActorWithCreditsRequest : IRequest<GetActorWithCreditsResponse>, IApolloType
    {
        public string ActorId { get; set; }
        public bool WithShow { get; set; }
        public GetActorWithCreditsRequest(string actorId, bool withShow = true)
        {
            ActorId = actorId;
            WithShow = withShow;
        }
    }

    public class GetActorWithCreditsResponse : IApolloType
    {
        public IEnumerable<PersonCredit> Credits { get; set; }
    }

    public class GetActorWithCreditsHandler : IRequestHandler<GetActorWithCreditsRequest, GetActorWithCreditsResponse>
    {
        private readonly TVMazeClient _tvMazeClient;

        public GetActorWithCreditsHandler(IOptions<ApiKeys> apiKeys, IOptions<ServicePath> servicePath, TVMazeClient tvMazeClient)
        {
            _tvMazeClient = tvMazeClient;
        }

        public async Task<GetActorWithCreditsResponse> Handle(GetActorWithCreditsRequest request, CancellationToken cancellationToken)
        {
            var model = new GetActorWithCreditsResponse();
            model.Credits = await _tvMazeClient.GetCollection<PersonCredit>($"people/{request.ActorId}/castcredits{(request.WithShow ? "?embed=show" : null)}");

            return model;
        }
    }
}

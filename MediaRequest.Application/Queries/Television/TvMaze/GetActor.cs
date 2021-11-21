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
    public class GetActorRequest : IRequest<GetActorResponse>, IApolloType
    {
        public string ActorId { get; set; }
        public bool WithShow { get; set; }
        public GetActorRequest(string actorId, bool withShow = true)
        {
            ActorId = actorId;
            WithShow = withShow;
        }
    }

    public class GetActorResponse : IApolloType
    {
        public Person Actor { get; set; }
    }

    public class GetActorHandler : IRequestHandler<GetActorRequest, GetActorResponse>
    {
        private readonly TVMazeClient _tvMazeClient;

        public GetActorHandler(TVMazeClient tvMazeClient)
        {
            _tvMazeClient = tvMazeClient;
        }

        public async Task<GetActorResponse> Handle(GetActorRequest request, CancellationToken cancellationToken)
        {
            var model = new GetActorResponse();
            model.Actor = await _tvMazeClient.GetSingle<Person>($"people/{request.ActorId}");

            return model;
        }
    }
}

using MediaRequest.Application.Clients;
using MediaRequest.Domain.API_Responses.TMDB;
using MediaRequest.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MediaRequest.Application.Queries.Find
{
    public enum ExternalSource 
    {
        TVDB,
        IMDb,
    }
    
    public class SearchByExternalIdRequest : IRequest<SearchByExternalIdResponse>, IApolloType
    {
        public ExternalSource Source { get; set; }
        public string Id { get; set; }
    }

    public class SearchByExternalIdResponse : IApolloType
    {
        public FindResult Result { get; set; }
    }

    public class SearchByExternalIdHandler : IRequestHandler<SearchByExternalIdRequest, SearchByExternalIdResponse>
    {
        private readonly TMDBClient _tmdbClient;

        public SearchByExternalIdHandler(TMDBClient tmdbClient)
        {
            _tmdbClient = tmdbClient;
        }

        public async Task<SearchByExternalIdResponse> Handle(SearchByExternalIdRequest request, CancellationToken cancellationToken)
        {
            var result = await _tmdbClient.GetSingle<FindResult>($"find/{request.Id}?external_source=tvdb_id");
            var model = new SearchByExternalIdResponse() { Result = result };

            return model;

            //if (result.Any())
            //{
            //    model.Result = model.Result;   
            //    return model;
            //} else
            //{
            //    model.Result = new FindResult();
            //    return model;
            //}
        }
    }
}

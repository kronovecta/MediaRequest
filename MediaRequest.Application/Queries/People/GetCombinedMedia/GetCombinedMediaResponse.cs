using MediaRequest.Domain.API_Responses;
using MediaRequest.Domain.TMDB;
using System.Collections.Generic;

namespace MediaRequest.Application.Queries.People.GetCombinedMedia
{
    public class GetCombinedMediaResponse
    {
        public Actor Actor { get; set; }
        public OtherWorks Movies { get; set; }
    }
}
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediaRequest.Application.Queries.People.GetCombinedMedia
{
    public class GetCombinedMediaRequest : IRequest<GetCombinedMediaResponse>
    {
        public string ActorID { get; set; }
    }
}

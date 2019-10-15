using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediaRequest.Application.Queries.Movies
{
    public class GetRecommendedRequest : IRequest<GetRecommendedResponse>
    {
        public string TMDBId { get; set; }
        public int? Page { get; set; }
    }
}

using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediaRequest.Application.Queries.Movies
{
    public class GetCreditsRequest : IRequest<GetCreditsResponse>
    {
        public int Amount { get; set; }
        public string TMDBId { get; set; }
    }
}

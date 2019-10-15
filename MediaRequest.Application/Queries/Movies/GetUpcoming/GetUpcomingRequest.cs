using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediaRequest.Application.Queries.Movies.GetUpcoming
{
    public class GetUpcomingRequest : IRequest<GetUpcomingResponse>
    {
        public int Days { get; set; }
    }
}

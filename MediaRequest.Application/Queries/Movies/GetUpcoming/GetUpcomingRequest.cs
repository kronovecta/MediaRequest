using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediaRequest.Application.Queries.Movies.GetUpcoming
{
    public class GetUpcomingRequest : IRequest<GetUpcomingResponse>
    {
        public GetUpcomingRequest(int? days = 90)
        {
            Days = days ?? 60;
        }

        public int Days { get; set; }
    }
}

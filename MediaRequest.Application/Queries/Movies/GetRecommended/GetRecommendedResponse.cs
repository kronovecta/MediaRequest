using MediaRequest.Domain.API_Responses;

namespace MediaRequest.Application.Queries.Movies
{
    public class GetRecommendedResponse
    {
        public Recommendations Recommendations { get; set; }
    }
}
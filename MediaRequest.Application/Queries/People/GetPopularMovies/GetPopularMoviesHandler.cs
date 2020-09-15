using MediaRequest.Domain.API_Responses.TMDB;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace MediaRequest.Application.Queries.People.GetPopularMovies
{
    public class GetPopularMoviesHandler : IRequestHandler<GetPopularMoviesRequest, GetPopularMoviesResponse>
    {
        private readonly IHttpHelper _http;

        public GetPopularMoviesHandler(IHttpHelper http)
        {
            _http = http;
        }

        public async Task<GetPopularMoviesResponse> Handle(GetPopularMoviesRequest request, CancellationToken cancellationToken)
        {
            var model = new GetPopularMoviesResponse();
            var result = await _http.GetPopularMovies(request.ActorId);

            using (var stream = await result.Content.ReadAsStreamAsync())
            {
                var data = await JsonSerializer.DeserializeAsync<PopularMovies>(stream);

                foreach (var movie in data.results)
                {
                    movie.backdrop_path = movie.backdrop_path != null ? movie.backdrop_path : "https://www.themoviedb.org/assets/2/v4/glyphicons/basic/glyphicons-basic-38-picture-grey-c2ebdbb057f2a7614185931650f8cee23fa137b93812ccb132b9df511df1cfac.svg";
                };

                model.Movies = data.results;
            }

            return model;
        }
    }
}

using MediaRequest.Application.Queries.Movies;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediaRequest.Application.Queries.Movies
{
    public class GetExistingMoviesRequest : IRequest<GetExistingMoviesResponse>
    {
    }

    public class GetExistingMoviesFilteredRequest : IRequest<GetExistingMoviesResponse>
    {
        private string _input = string.Empty;
        public string Input
        {
            get
            {
                return _input;
            }
            set
            {
                
                _input = value.ToLower();
            }
        }
    }
}

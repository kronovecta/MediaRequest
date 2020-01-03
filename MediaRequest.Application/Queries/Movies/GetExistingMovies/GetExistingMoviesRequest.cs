using MediaRequest.Application.Queries.Movies;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediaRequest.Application.Queries.Movies
{
    public class GetExistingMoviesRequest : IRequest<GetExistingMoviesResponse>
    {
        public int CurrentPage { get; set; }
        public int Amount { get; set; }
    }

    public class GetExistingMoviesFilteredRequest : IRequest<GetExistingMoviesResponse>
    {
        //private string _input = string.Empty;
        //public string Input
        //{
        //    get
        //    {
        //        return _input;
        //    }
        //    set
        //    {

        //        _input = value.ToLower();
        //    }
        //}

        public string Input { get; set; }
        public int FilterMode { get; set; }
        public int CurrentPage { get; set; }
        public int Amount { get; set; }
    }
}

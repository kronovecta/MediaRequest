using MediaRequest.Domain;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaRequest.WebUI.ViewModels
{
    public class MoviesMovieUserViewModel
    {
        // View
        public IEnumerable<Movie> Movies { get; set; }

        // Form return
        public MovieUserViewModel Model { get; set; }
    }

    public class MovieUserViewModel
    {
        public IdentityUser User { get; set; }
        public Movie Movie { get; set; }
    }
}

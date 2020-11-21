using MediaRequest.Domain.TMDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaRequest.WebUI.ViewModels
{
    public class MovieCreditsViewModel
    {
        public string TMDBId { get; set; }
        public Credits Credits { get; set; }
    }
}

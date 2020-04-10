using MediaRequest.Domain.Radarr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaRequest.WebUI.ViewModels.SingleMovie
{
    public class MovieViewModel
    {
        public bool Requested { get; set; }
        public bool Accepted { get; set; }
        public Movie Movie { get; set; }
    }
}

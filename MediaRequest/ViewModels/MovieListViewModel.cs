﻿using MediaRequest.Domain.Radarr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaRequest.WebUI.ViewModels
{
    public class MovieListViewModel
    {
        
        public IEnumerable<Movie> Movies { get; set; }
    }
}

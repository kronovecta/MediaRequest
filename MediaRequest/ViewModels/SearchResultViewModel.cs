﻿using MediaRequest.Domain;
using MediaRequest.Domain.Radarr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaRequest.WebUI.ViewModels
{
    public class SearchResultViewModel
    {
        public List<MovieExists> Movies { get; set; }

        public SearchResultViewModel()
        {
            Movies = new List<MovieExists>();
        }
    }

    public class MovieExists
    {
        public Movie Movie { get; set; }
        public bool Exists { get; set; }
        public bool Monitored { get; set; }
    }
}

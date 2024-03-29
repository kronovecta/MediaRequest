﻿using MediaRequest.Domain;
using MediaRequest.Domain.API_Responses.Radarr.v3;
using MediaRequest.WebUI.Models.IdentityModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaRequest.WebUI.ViewModels
{
    public class DistinctMovieUserRequestViewModel
    {
        public DistinctMovieUserRequestViewModel()
        {
            Requests = new List<MovieUserRequestViewModel>();
        }

        public bool Status { get; set; } = false;
        public Movie Movie { get; set; }
        public List<MovieUserRequestViewModel> Requests { get; set; }
    }

    public class MovieUserRequestViewModel
    {
        public ApplicationUser User { get; set; }
        public UserRequest Request { get; set; }
    }
}

﻿using MediaRequest.Domain;
using MediaRequest.Domain.API_Responses.Radarr.v3;
using MediaRequest.WebUI.Models.IdentityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaRequest.WebUI.ViewModels.Profile
{
    public class ProfileViewModel
    {
        public ApplicationUser User { get; set; }
        public List<RequestViewModel> Requests { get; set; }
        public Movie Upcoming { get; set; }
    }

    public class RequestViewModel
    {
        public UserRequest Request { get; set; }
        public Movie Movie { get; set; }
    }
}

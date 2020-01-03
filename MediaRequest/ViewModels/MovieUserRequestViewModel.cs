using MediaRequest.Domain;
using MediaRequest.Domain.Radarr;
using MediaRequest.WebUI.Models.IdentityModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaRequest.WebUI.ViewModels
{
    public class MovieUserRequestViewModel
    {
        public Movie Movie { get; set; }
        public ApplicationUser User { get; set; }
        public UserRequest Request { get; set; }
    }
}

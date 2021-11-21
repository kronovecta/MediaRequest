using MediaRequest.Domain;
using MediaRequest.Domain.API_Responses.Radarr.v3;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaRequest.WebUI.ViewModels.Admin
{
    public class AdminPanelViewModel
    {
        public int Requests { get; set; }
        public int Reminders { get; set; }
        public int Members { get; set; }
        public AdminPanelRquestMovieViewModel LatestRequest { get; set; }
        public Movie NextUpcomingMovie { get; set; }
        public int TotalMovies { get; set; }
    }

    public class AdminPanelRquestMovieViewModel
    {
        public Movie Movie { get; set; }
        public IdentityUser User { get; set; }

        public AdminPanelRquestMovieViewModel()
        {
            Movie = new Movie();
        }
    }
}

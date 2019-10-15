using MediaRequest.Domain;
using MediaRequest.Domain.Radarr;
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
        public UserRequest LatestRequest { get; set; }
        public Movie NextUpcomingMovie { get; set; }
        public int TotalMovies { get; set; }
    }
}

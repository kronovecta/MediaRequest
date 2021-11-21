using MediaRequest.Domain.API_Responses.Radarr.v3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaRequest.WebUI.ViewModels.Profile
{
    public class CalendarViewModel
    {
        public IEnumerable<Movie> Events { get; set; }
    }
}

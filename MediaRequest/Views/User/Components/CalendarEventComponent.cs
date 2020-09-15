using MediaRequest.Domain.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaRequest.WebUI.Views.User.Components
{
    [ViewComponent(Name = "CalendarEvent")]
    public class CalendarEventComponent : ViewComponent
    {
        private readonly ServicePath _path;


        public CalendarEventComponent(IOptions<ServicePath> path)
        {
            _path = path.Value;
        }

        public async Task<IViewComponentResult> InvokeAsync(int maxPriority, MediaRequest.Domain.Radarr.Movie movie)
        {
            movie.Images.ForEach(x => x.URL = _path.BaseURL + x.URL);
            return View(movie);
        }
    }
}

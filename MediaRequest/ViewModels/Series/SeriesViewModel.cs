using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaRequest.WebUI.ViewModels.Series
{
    public class SeriesViewModel
    {
        public bool Requested { get; set; }
        public bool Accepted { get; set; }
        public MediaRequest.Domain.API_Responses.Sonarr.Series Series { get; set; }
        public bool AlreadyAdded => Series.Added == DateTime.MinValue ? false : true;
    }
}

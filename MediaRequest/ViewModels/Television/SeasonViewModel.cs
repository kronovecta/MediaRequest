using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaRequest.WebUI.ViewModels.Television
{
    public class SeasonViewModel
    {
        public int Number { get; set; }
        public string Name { get; set; }
        public bool Monitored { get; set; }
        public int TvdbId { get; set; }
        public string Overview { get; set; }
        public int MyProperty { get; set; }
    }
}

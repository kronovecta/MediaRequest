using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaRequest.WebUI.ViewModels.Admin
{
    public class SettingsViewModel
    {
        public string RootPath { get; set; }
        public string RadarrPath { get; set; }
        
        public string Radarr_APIKey { get; set; }
        public string TMDB_APIKey { get; set; }
    }
}

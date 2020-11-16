using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaRequest.WebUI.ViewModels.Admin
{
    public class AdminSettingsViewModel
    {
        #region Base Settings
        public string BaseUrl { get; set; }
        #endregion

        #region Radarr
        public string RadarrUrl { get; set; }
        public string RadarrAPIKey { get; set; }
        #endregion

        #region TMDB
        public string TmdbApiKey { get; set; }
        #endregion
    }
}

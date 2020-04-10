﻿using MediaRequest.Domain.Radarr;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediaRequest.Domain
{
    public class MovieRequestObject
    {
        public string title { get; set; }
        public int qualityProfileId { get; set; }
        public string titleSlug { get; set; }
        public List<Image> images { get; set; }
        public string tmdbId { get; set; }
        public int year { get; set; }
        public string path { get; set; }
        public MovieRequestObjectOptions addOptions => new MovieRequestObjectOptions();
    }

    public class MovieRequestObjectOptions
    {
        // API requires string. Always convert
        public string searchForMovie { get; set; }
    }
}

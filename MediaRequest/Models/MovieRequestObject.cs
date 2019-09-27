﻿using MediaRequest.Domain;
using MediaRequest.Domain.Radarr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaRequest.WebUI.Models
{
    public class MovieRequestObjectOld
    {
        public string title { get; set; }
        public int qualityProfileId { get; set; }
        public string titleSlug { get; set; }
        public List<Image> images { get; set; }
        public string tmdbId { get; set; }
        public int year { get; set; }
        public string path { get; set; }
    }
}

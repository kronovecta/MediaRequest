using System;
using System.Collections.Generic;
using System.Text;

namespace MediaRequest.Domain
{
    public class MoviePoster
    {
        public int MoviePosterId { get; set; }
        public string MovieId { get; set; }
        public string PosterUrl { get; set; }
        public string FanartUrl { get; set; }
    }
}

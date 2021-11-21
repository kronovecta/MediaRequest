using System;
using System.Collections.Generic;
using System.Text;

namespace MediaRequest.Domain
{
    public class TelevisionPoster
    {
        public int PosterId { get; set; }
        public string SeriesId { get; set; }
        public string PosterUrl { get; set; }
        public string FanartUrl { get; set; }
        public string BannerUrl { get; set; }
    }
}

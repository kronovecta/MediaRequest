using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaRequest.Domain
{
    public class Movie
    {
        public string Title { get; set; }
        public List<object> AlternativeTitles { get; set; }
        public int SecondaryYearSourceId { get; set; }
        public string SortTitle { get; set; }
        public string SizeOnDisk { get; set; }
        public string Status { get; set; }
        public string Overview { get; set; }
        public DateTime InCinemas { get; set; }
        public List<Image> Images { get; set; }
        public bool Downloaded { get; set; }
        public string RemotePoster { get; set; }
        public int Year { get; set; }
        public bool HasFile { get; set; }
        public int ProfileId { get; set; }
        public string PathState { get; set; }
        public bool Monitored { get; set; }
        public string MinimumAvailability { get; set; }
        public bool IsAvailable { get; set; }
        public string FolderName { get; set; }
        public int Runtime { get; set; }
        public string TMDBId { get; set; }
        public string TitleSlug { get; set; }
        public List<object> Genres { get; set; }
        public List<object> Tags { get; set; }
        public DateTime Added { get; set; }
        public Ratings Ratings { get; set; }
        public int QualityProfileId { get; set; }
    }
}

﻿using MediaRequest.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MediaRequest.Domain.Radarr
{
    public class Movie : IRadarrType
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<AlternativeTitle> AlternativeTitles { get; set; }
        public int SecondaryYearSourceId { get; set; }
        public string SortTitle { get; set; }
        public UInt64 SizeOnDisk { get; set; }
        public string Status { get; set; }
        public string Overview { get; set; }
        public DateTime InCinemas { get; set; }
        public DateTime PhysicalRelease { get; set; }
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
        public int TMDBId { get; set; }
        public List<string> Genres { get; set; }
        public List<object> Tags { get; set; }
        public DateTime Added { get; set; }
        public Ratings Ratings { get; set; }
        public int QualityProfileId { get; set; }
        public string PosterUrl { get; set; }
        public string FanartUrl { get; set; }
        public DateTime lastInfoSync { get; set; }
        public string youTubeTrailerId { get; set; }
        public int LocalScore { get; set; }
        public string Studio { get; set; }
        public bool AlreadyAdded { get; set; } = false;

        // Generates a slug
        [JsonIgnore]
        public string TitleSlug
        {
            get
            {
                var stripped = Regex.Replace(Title.ToLower(), "[^A-Za-z0-9 ]+", "-", RegexOptions.Compiled);
                
                return string.Format("{0}-{1}", stripped, TMDBId);
            }
        }
    }

    public class Genre
    {
        public string GenreName { get; set; }
    }

    public class Image
    {
        public string CoverType { get; set; }
        public string URL { get; set; }
    }

    public class Ratings
    {
        public int Votes { get; set; }
        public double Value { get; set; }
    }


    public class AlternativeTitle
    {
        public string sourceType { get; set; }
        public int movieId { get; set; }
        public string title { get; set; }
        public int sourceId { get; set; }
        public int votes { get; set; }
        public int voteCount { get; set; }
        public string language { get; set; }
    }
}

﻿using MediaRequest.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace MediaRequest.Domain.TMDB
{
    public class Cast : ICreditObject
    {
        public int Cast_Id { get; set; }
        public string Character { get; set; }
        public string Credit_id { get; set; }
        public int Gender { get; set; }
        public int ID { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public string Profile_path { get; set; }
        public string PortraitPath => "https://image.tmdb.org/t/p/w200" + Profile_path;

        // Generates a slug
        [JsonIgnore]
        public string Slug
        {
            get
            {
                return string.Format("{0}-{1}", Name.ToLower().Replace(" ", "-"), ID);
            }
        }
    }

    public class Crew : ICreditObject
    {
        public string Credit_id { get; set; }
        public string Department { get; set; }
        public int Gender { get; set; }
        public int ID { get; set; }
        public string Job { get; set; }
        public string Name { get; set; }
        public string Profile_path { get; set; }
    }

    public class Credits
    {
        public int Id { get; set; }
        public List<Cast> Cast { get; set; }
        public List<Crew> Crew { get; set; }
    }
}

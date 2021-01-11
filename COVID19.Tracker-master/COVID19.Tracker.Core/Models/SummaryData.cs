using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace COVID19.Tracker.Core.Models
{
    public class Global
    {
        public int NewConfirmed { get; set; }
        public int TotalConfirmed { get; set; }
        public int NewDeaths { get; set; }
        public int TotalDeaths { get; set; }
        public int NewRecovered { get; set; }
        public int TotalRecovered { get; set; }
    }

    public class Premium
    {
    }



    public class Country
    {
        [JsonProperty("Country")]
        public string CountryName { get; set; }
        public string CountryCode { get; set; }
        public string Slug { get; set; }
        public int NewConfirmed { get; set; }
        public int TotalConfirmed { get; set; }
        public int NewDeaths { get; set; }
        public int TotalDeaths { get; set; }
        public int NewRecovered { get; set; }
        public int TotalRecovered { get; set; }
        public DateTime Date { get; set; }
        public Premium Premium { get; set; }
        [JsonProperty("Countries")]
        public List<Country> Subdivision { get; set; }
    }


    public class Stats
    {
        public string Message { get; set; }
        public Global Global { get; set; }
        public List<Country> Countries { get; set; }
        public DateTime Date { get; set; }
    }
}

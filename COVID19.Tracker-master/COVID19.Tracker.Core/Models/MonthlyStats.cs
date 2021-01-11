using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace COVID19.Tracker.Core.Models
{
    public class MonthlyStat
    {
        public string Country { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string CountryCode { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Province { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string City { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string CityCode { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Lat { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Lon { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int Cases { get; set; }
        public string Status { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public DateTime Date { get; set; }
    }
}

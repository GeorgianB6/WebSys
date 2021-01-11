using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace COVID19.Tracker.Core.Models
{
    public class State
    {
        [JsonProperty("ISO2")]
        public string Code { get; set; }
        [JsonProperty("Country")]
        public string Name { get; set; }
    }
}

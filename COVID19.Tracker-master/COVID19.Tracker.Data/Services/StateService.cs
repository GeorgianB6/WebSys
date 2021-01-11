using COVID19.Tracker.Core.Models;
using COVID19.Tracker.Core.Services;
using COVID19.Tracker.Data.Constants;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace COVID19.Tracker.Data.Services
{
    public class StateService : IStateService
    {
        public async Task<List<State>> GetAllStates()
        {
            var allStates = new List<State>();
            var API_URL = Data.Constants.API.BaseURL;
            using (var client = new HttpClient())
            {
                string url = $"{API_URL}countries";
                var response = client.GetAsync(url).Result;
                string responseAsString = await response.Content.ReadAsStringAsync();
                allStates = JsonConvert.DeserializeObject<List<State>>(responseAsString);
            }
            return allStates;
        }
    }
}

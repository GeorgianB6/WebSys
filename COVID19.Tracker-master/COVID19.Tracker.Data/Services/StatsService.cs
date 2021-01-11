using COVID19.Tracker.Core.Models;
using COVID19.Tracker.Core.Models.COVIDStats;
using COVID19.Tracker.Core.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace COVID19.Tracker.Data.Services
{
    public class StatsService : IStatsService
    {

        public async Task<Country> GetStatsAsync(string code)
        {
            var summaryData = new Stats();
            var API_URL = Data.Constants.API.BaseURL;
            using (var client = new HttpClient())
            {
                string url = $"{API_URL}summary";
                var response = client.GetAsync(url).Result;
                string responseAsString = await response.Content.ReadAsStringAsync();
                //apiResult = JsonConvert.DeserializeObject<Dictionary<string, Stats>>(responseAsString);
                summaryData = JsonConvert.DeserializeObject<Stats>(responseAsString); 
            }
            if (code.Equals("GLOBE"))
            {
                return new Country
                {
                    CountryName = "Global",
                    CountryCode = "GLOBE",
                    Slug = "global",
                    NewConfirmed = summaryData.Global.NewConfirmed,
                    TotalConfirmed = summaryData.Global.TotalConfirmed,
                    NewDeaths = summaryData.Global.NewDeaths,
                    TotalDeaths = summaryData.Global.TotalDeaths,
                    NewRecovered = summaryData.Global.NewRecovered,
                    TotalRecovered = summaryData.Global.TotalRecovered,
                    Date = summaryData.Date,
                    Subdivision = summaryData.Countries
                };
            }
            else
            {
                var data = summaryData.Countries.Where(a => a.CountryCode == code).FirstOrDefault();
                return data;
            }   
        }
        public async Task<Country> GetStatsByDateAsync(string code, DateTime dateTime)
        {
            var summaryData = new Stats();
            var API_URL = Data.Constants.API.BaseURL;
            using (var client = new HttpClient())
            {
                var dateTimeString = dateTime.ToString("yyyy-MM-dd");
                string url = $"{API_URL}data-{dateTimeString}.min.json";
                var response = client.GetAsync(url).Result;
                string responseAsString = await response.Content.ReadAsStringAsync();
                summaryData = JsonConvert.DeserializeObject<Stats>(responseAsString);
            }
            if (code.Equals("GLOBE"))
            {
                return new Country
                {
                    CountryName = "Global",
                    CountryCode = "GLOBE",
                    Slug = "global",
                    NewConfirmed = summaryData.Global.NewConfirmed,
                    TotalConfirmed = summaryData.Global.TotalConfirmed,
                    NewDeaths = summaryData.Global.NewDeaths,
                    TotalDeaths = summaryData.Global.TotalDeaths,
                    NewRecovered = summaryData.Global.NewRecovered,
                    TotalRecovered = summaryData.Global.TotalRecovered,
                    Date = summaryData.Date
                };
            }
            else
            {
                var data = summaryData.Countries.Where(a => a.CountryCode == code).FirstOrDefault();
                return data;
            }
        }

        public async Task<List<int>> GetMonthlyData(string code)
        {
            var monthlyValues = new List<int>();
            try
            {
                var API_URL = Data.Constants.API.BaseURL;
            DateTime endDate = DateTime.Parse("12/30/2020");
            DateTime startDate = DateTime.Parse("01/01/2020");
            DateTime endMonthDate = DateTime.Parse("01/25/2020");
            
            using var client = new HttpClient();
            string url = $"{API_URL}total/country/{code}/status/confirmed?from={startDate.ToString("yyyy-dd-MM'T'HH:mm:ss'Z'")}&to={endDate.AddSeconds(1).ToString("yyyy-dd-MM'T'HH:mm:ss'Z'")}";
            var response = client.GetAsync(url).Result;
            string responseAsString = await response.Content.ReadAsStringAsync();
            
            var myDeserializedClass = JsonConvert.DeserializeObject<MonthlyStat[]>(responseAsString);
            for(var i=1; i<=12; i++)
                {
                    foreach(var c in myDeserializedClass)
                    {
                        if (c.Date == endMonthDate)
                        {
                            monthlyValues.Add(c.Cases);
                        }
                    }
                    endMonthDate = endMonthDate.AddMonths(1);
                }
            }
            catch(Exception ex) 
            { }
            return monthlyValues;
        }

    }
}

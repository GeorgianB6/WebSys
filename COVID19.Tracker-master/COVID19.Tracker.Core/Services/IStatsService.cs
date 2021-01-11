using COVID19.Tracker.Core.Models;
using COVID19.Tracker.Core.Models.COVIDStats;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace COVID19.Tracker.Core.Services
{
    public interface IStatsService
    {
        public Task<Country> GetStatsAsync(string code);
        public Task<Country> GetStatsByDateAsync(string code, DateTime dateTime);
        public Task<List<int>> GetMonthlyData(string code);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using COVID19.Tracker.Core.Models;
using COVID19.Tracker.Core.Models.COVIDStats;
using COVID19.Tracker.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace COVID19.Tracker.Controllers
{

    [ApiController]
    public class StatsController : ControllerBase
    {
        private readonly IStatsService statsService;

        public StatsController(IStatsService statsService)
        {
            this.statsService = statsService;
        }
        [HttpGet]
        [Route("api/stats")]
        public async Task<IActionResult> GetStatsByCodeAsync([FromQuery] string code)
        {
            var data = new Country();          
            data = await statsService.GetStatsAsync(code);
            return Ok(data);
        }
        [HttpGet]
        [Route("api/monthly")]
        public async Task<IActionResult> GetMonthlyData([FromQuery] string code)
        {
            var data = new List<int>();
            data = await statsService.GetMonthlyData(code);
            return Ok(data);
        }

        /* public async Task<IActionResult> GetStatsByDateAsync([FromQuery] string code, [FromQuery] DateTime date)
         {
             var data = new Country();
             if (date < DateTime.Now)
             {
                 var baseDate = new DateTime(2020, 1, 1);
                 if (baseDate > date || date == DateTime.Today)
                 {
                     data = await statsService.GetStatsAsync(code);
                 }
                 else
                 {
                     data = await statsService.GetStatsByDateAsync(code, date);
                 }
             }
             return Ok(data);
         }*/
    }
}
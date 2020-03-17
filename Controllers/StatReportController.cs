using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using dg.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System;

namespace dg.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StatReportController : ControllerBase
    {

        private IStatsService _statsService;

        private readonly ILogger<StatReportController> _logger;

        public StatReportController(ILogger<StatReportController> logger, IStatsService statsService)
        {
            _logger = logger;
            _statsService = statsService;
        }

        // [Authorize]
        [HttpGet]
        public Task<StatReport> GetAsync([FromQuery]string selectedCountry, [FromQuery]string selectedCountryCode, [FromQuery]string ip)
        {
            return _statsService.GetAsync(selectedCountry, selectedCountryCode, ip);
        }
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using dg.Interfaces;
using Microsoft.AspNetCore.Authorization;

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
        [HttpGet("{country}")]
        public Task<IEnumerable<StatReport>> GetAsync(string country)
        {
            return _statsService.GetAsync(country);
        }
    }
}

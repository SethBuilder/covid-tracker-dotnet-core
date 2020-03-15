using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace dg.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StatReportController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<StatReportController> _logger;

        public StatReportController(ILogger<StatReportController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{country}")]
        public async Task<IEnumerable<StatReport>> GetAsync(string country)
        {
            Console.WriteLine(country);
            var client = new RestClient($"https://covid19-api.weedmark.systems/api/v1/stats?country={country}");
            var request = new RestRequest(Method.GET);
            IRestResponse response = await client.ExecuteAsync(request);
            var content = JsonConvert.DeserializeObject<JToken>(response.Content);

            return Enumerable.Range(0, 1).Select(index => new StatReport
            {
                Date = (DateTime) content["data"]["covid19Stats"][index]["lastUpdate"],
                Confirmed = (int) content["data"]["covid19Stats"][index]["confirmed"],
                Recovered = (int) content["data"]["covid19Stats"][index]["recovered"],
                Deaths = (int) content["data"]["covid19Stats"][index]["deaths"],
            })
            .ToArray();
        }
    }
}

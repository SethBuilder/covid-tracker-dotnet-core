using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dg.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace dg.Services
{
    public class StatsService : IStatsService
    {
        public async Task<IEnumerable<StatReport>> GetAsync(string country) {
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
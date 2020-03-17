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
        public Task<StatReport> GetAsync(string selectedCountry,string selectedCountryCode, string ip) {
            dynamic country = new System.Dynamic.ExpandoObject();
            if(selectedCountry != null) {
                country.label = selectedCountry;
                country.code = selectedCountryCode;
            } else {
                country = this.getVountryFromIp(ip);
            }
            
            if(country.label == "United States of America" || country.label == "United States") {
                country.label = "US";
            }

            if(country.label == "Russian Federation") {
                country.label = "Russia";
            }

            if(country.label == "Palestine, State of") {
                country.label = "occupied Palestinian territory";
            }

            if(country.label == "Iran, Islamic Republic of") {
                country.label = "Iran";
            }

            if(country.label == "Macedonia, the Former Yugoslav Republic of") {
                country.label = "North Macedonia";
            }

            
            dynamic content = this.getJson($"https://covid19-api.weedmark.systems/api/v1/stats?country={country.label}");
            List<StatReport> items = content.data.covid19Stats.ToObject<List<StatReport>>();
            int sumConfirmed = 0;
            int sumRecovered = 0;
            int sumDeaths = 0;
            bool contains = (country.label).Contains(items[0].Country) || (country.label).IndexOf(items[0].Country) > -1;
            if(contains) {
                foreach (StatReport el  in items)
                {
                    sumConfirmed += el.Confirmed;
                    sumRecovered += el.Recovered;
                    sumDeaths += el.Deaths;
                }
            }

            StatReport report = new StatReport();
            report.Deaths = sumDeaths;
            report.Confirmed = sumConfirmed;
            report.Recovered = sumRecovered;
            report.lastUpdate = content.data.lastChecked;
            report.Country = country;
            return Task.FromResult(report);

        }

        private dynamic getVountryFromIp(string ip) {

            string countryCodeFromIp = 
                this.getJson($"https://freegeoip.app/json/{ip}").country_code;
            string countryNameFromCode = 
                this.getJson($"https://restcountries.eu/rest/v2/alpha/{countryCodeFromIp}").name;
            
            dynamic countryData = new System.Dynamic.ExpandoObject();
            countryData.code = countryCodeFromIp;
            countryData.label = countryNameFromCode;
            return countryData;
        }

        private dynamic getJson(string url) {
            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            dynamic content = JsonConvert.DeserializeObject(response.Content);
            return content;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dg.Interfaces
{
    public interface IStatsService
    {
        Task<StatReport> GetAsync(string selectedCountry, string selectedCountryCode, string ip);
    }
}
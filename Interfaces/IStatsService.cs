using System.Collections.Generic;

namespace dg.Interfaces
{
    public interface IStatsService
    {
        System.Threading.Tasks.Task<IEnumerable<StatReport>> GetAsync(string country);
    }
}
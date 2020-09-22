using Q2.DataCollector;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SRA.Client.Service
{
    public interface IExperimentService
    {
        Task<Experiment[]> Get();
    }
}

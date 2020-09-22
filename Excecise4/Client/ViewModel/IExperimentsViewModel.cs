using SRA.Client.Models;
using System;
using System.Threading.Tasks;

namespace SRA.Client.ViewModel
{
    public interface IExperimentsViewModel
    {
        ExperimentModelCollection Experiments { get; }
        event EventHandler ModelUpdated;

        void DeleteExperiment(Guid id);
    }
}
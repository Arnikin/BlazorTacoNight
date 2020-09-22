using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using SRA.Client.Models;

namespace SRA.Client.Models
{
    public interface IModelRepository
    {
        ExperimentModelCollection Experiments { get; }
        PlateModelCollection Plates { get; }

        void SaveExperimentDetails(ExperimentModel experiment);

        event EventHandler PlatesChanged;
        event EventHandler ExperimentsChanged;
    }
}

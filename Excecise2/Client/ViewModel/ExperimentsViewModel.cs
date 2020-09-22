using Microsoft.AspNetCore.Components;
using SRA.Client.Models;
using SRA.Client.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SRA.Client.ViewModel
{
    public class ExperimentsViewModel: IExperimentsViewModel
    {
        IModelRepository Repo;
        public ExperimentsViewModel(IModelRepository repo)
        {
            Repo = repo;
            Repo.ExperimentsChanged += Repo_ExperimentsChanged;
        }

        private void Repo_ExperimentsChanged(object sender, EventArgs e)
        {
            OnModelUpdated();
        }

        private IExperimentService Service { get; set; }

        public ExperimentModelCollection Experiments => Repo.Experiments;

        public event EventHandler ModelUpdated;

        private void OnModelUpdated()
        {
            ModelUpdated?.Invoke(this, EventArgs.Empty);
        }

        public void DeleteExperiment(Guid id)
        {
            var toDelete = Experiments.FirstOrDefault(x => x.Id == id);
            if (toDelete != null)
            {
                Experiments.Remove(toDelete);

                OnModelUpdated();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Q2.DataCollector;
using SRA.Client.Models;
using SRA.Client.Service;

namespace SRA.Client.Models
{
    public class ModelRepository : IModelRepository
    {
        public ModelRepository(IExperimentService experimentService)
        {
            ExperimentService = experimentService;

            this.Experiments.RemovingItem += Experiments_RemovingItem;

            Task.Run(() => GetExperimentsFromService());
            Task.Run(() => GetPlatesFromService());
        }

        private void Experiments_RemovingItem(object sender, RemoveItemEventArgs<ExperimentModel> e)
        {
            var plates = this.Plates.Where(p => e.RemovedItem.Id == p.Experiment?.Id);

            if (plates != null)
            {
                foreach (var plate in plates)
                {
                    plate.Experiment = null;
                }
            }
        }

        private IExperimentService ExperimentService { get; set; }

        private async Task GetExperimentsFromService()
        {
            var experiments = await ExperimentService.Get();

            foreach (var exp in experiments)
            {
                var expModel = _experiments.FirstOrDefault(e => e.Name == exp.Name);
                if (expModel == null)
                {
                    expModel = new ExperimentModel();
                    _experiments.Add(expModel);
                }

                expModel.Name = exp.Name;
                expModel.MeasurementSize = 0;
            }

            OnExperimentsChanged();
        }

        private void GetPlatesFromService()
        {
            for (int i = 0; i < 16; i++)
            {
                _plates.Add(new PlateModel() { Number = i + 1 });
            }
            OnPlatesChanged();
        }

        public void SaveExperimentDetails(ExperimentModel experiment)
        {
            var toUpdate = Experiments.FirstOrDefault(x => x.Id == experiment.Id);
            if (toUpdate == null)
            {
                Experiments.Add(experiment);
            }
            else
            {
                toUpdate.Save(experiment);
            }

            OnExperimentsChanged();
            OnPlatesChanged();
        }

        ExperimentModelCollection _experiments = new ExperimentModelCollection();

        public ExperimentModelCollection Experiments
        {
            get
            {
                return _experiments;
            }
        }

        PlateModelCollection _plates = new PlateModelCollection();

        public PlateModelCollection Plates { 
            get 
            {
                return _plates;
            }
        }

        public event EventHandler PlatesChanged;
        public event EventHandler ExperimentsChanged;

        protected virtual void OnPlatesChanged()
        {
            PlatesChanged?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnExperimentsChanged()
        {
            ExperimentsChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}

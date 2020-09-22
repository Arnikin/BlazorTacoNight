using SRA.Client.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace SRA.Client.ViewModel
{
    public class ExperimentPlate
    {
        public int Number { get; set; }
        public bool IsDisabled { get; set; }
        public bool IsSelected { get; set; }
        public string Title { get; set; }
    }

    public class ExperimentViewModel : IExperimentViewModel
    {
        IModelRepository Repo;
        public ExperimentViewModel(IModelRepository repo)
        {
            Repo = repo;

            Progress = 0;
            Step = 1;

        }

        public int Progress { get; set; }

        public int Step { get; set; }

        public bool PreviousButtonDisabled { get; set; }

        public bool NextButtonDisabled { get; set; }

        public string Name { get; set; }
        public int Size { get; set; }

        public interval MeasurementInterval { get; set; }

        public traySize TraySize { get; set; }
        
        private PlateModelCollection Plates => Repo.Plates;

        Dictionary<int, List<ExperimentPlate>> _plateRows;
        public Dictionary<int, List<ExperimentPlate>> PlateRows
        {
            get
            {
                if (_plateRows == null)
                {
                    _plateRows = new Dictionary<int, List<ExperimentPlate>>(16);
                    for (int i = 0; i < 4; i++)
                    {
                        _plateRows.Add(i, new List<ExperimentPlate>());
                        for (int j = 0; j < 4; j++)
                        {
                            var plate = this.Plates[i * 4 + j];
                            _plateRows[i].Add(new ExperimentPlate() {Number=plate.Number
                                , Title=plate.Title
                                , IsDisabled=plate.Experiment!=null && (plate.Experiment.Id != this.Experiment.Id)
                                , IsSelected= plate.Experiment!=null && (plate.Experiment.Id == this.Experiment.Id)
                            });
                        }
                    }
                }

                return _plateRows;
            }
        }

        private ExperimentModel Experiment { get; set; }

        public IEnumerable<(interval Id, string Title)> MeasurementIntervals => Enum.GetValues(typeof(interval)).Cast<interval>().Select(mode => (mode, ((DescriptionAttribute)Attribute.GetCustomAttribute(typeof(interval).GetField(Enum.GetName(typeof(interval), mode)), typeof(DescriptionAttribute))).Description));

        public IEnumerable<(traySize Id, string Title)> TraySizes => Enum.GetValues(typeof(traySize)).Cast<traySize>().Select(mode => (mode, ((DescriptionAttribute)Attribute.GetCustomAttribute(typeof(traySize).GetField(Enum.GetName(typeof(traySize), mode)), typeof(DescriptionAttribute))).Description));

        public void SetExperiment(Guid experimentId)
        {
            if (experimentId == Guid.Empty)
            {
                Experiment = new ExperimentModel();
            }
            else
            {
                Experiment = Repo.Experiments.FirstOrDefault(x => x.Id == experimentId);
                Name = Experiment.Name;
                Size = Experiment.MeasurementSize;
                MeasurementInterval = Experiment.MeasurementInterval;
                TraySize = Experiment.TraySize;
            }
        }

        private void SetNavButtons()
        {
            NextButtonDisabled = false;
            switch (Step)
            {
                case 1:
                    PreviousButtonDisabled = true;
                    NextButtonDisabled = false;
                    break;
                case 2:
                    PreviousButtonDisabled = false;
                    NextButtonDisabled = false;
                    break;
                case 3:
                    PreviousButtonDisabled = false;
                    NextButtonDisabled = false;
                    break;
                case 4:
                    PreviousButtonDisabled = false;
                    NextButtonDisabled = true;
                    break;
                default:
                    break;
            }
        }

        public void GoToNextStep()
        {
            Step++;
            Progress += 25;
            if (Step == 4)
            {
                Progress = 100;
            }
            SetNavButtons();
        }

        public void GoToPreviousStep()
        {
            Step--;
            Progress -= 25;
            if (Step == 1)
            {
                Progress = 0;
            }
            SetNavButtons();
        }

        public void SetName()
        {
            Experiment.Name = this.Name;
        }

        public void SetSize()
        {
            Experiment.MeasurementSize = this.Size;
        }

        public void SetPlateSelected(int key, int number)
        {
            var plate = PlateRows[key].FirstOrDefault(x => x.Number == number);

            if (plate != null)
            {
                if (plate.IsSelected)
                {
                    Plates[number - 1].Experiment = null;
                }
                else
                {
                    Plates[number - 1].Experiment = this.Experiment;
                }

                plate.IsSelected = !plate.IsSelected;
            }
        }

        public void SetMeasurementInterval(interval item)
        {
            Console.WriteLine($"Option button clicked: {item.ToString()}");
            Experiment.MeasurementInterval = item;
        }
        public void SetTraySize(traySize item)
        {
            Experiment.TraySize = item;
        }
        public bool Confirm()
        {
            Console.WriteLine(Experiment.Name);
            Console.WriteLine(Experiment.MeasurementInterval.ToString());
            Repo.SaveExperimentDetails(this.Experiment);
            return true;
        }

        public void Cancel()
        {

        }

        private void Clean()
        {

        }

    }
}

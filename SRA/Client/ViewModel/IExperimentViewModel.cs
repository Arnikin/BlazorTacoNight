using SRA.Client.Models;
using System;
using System.Collections.Generic;

namespace SRA.Client.ViewModel
{
    public interface IExperimentViewModel
    {
        int Progress { get; }
        int Step { get; }
        bool PreviousButtonDisabled { get; }
        bool NextButtonDisabled { get; }

        string Name { get; set; }
        int Size { get; set; }
        interval MeasurementInterval { get; set; }

        Dictionary<int, List<ExperimentPlate>> PlateRows { get;}
        IEnumerable<(traySize Id, string Title)> TraySizes { get; }

        IEnumerable<(interval Id, string Title)> MeasurementIntervals { get; }
        traySize TraySize { get; set; }

        void GoToNextStep();
        void GoToPreviousStep();

        void SetExperiment(Guid experimentId);
        void SetName();
        void SetSize();
        void SetMeasurementInterval(interval item);
        void SetPlateSelected(int index, int number);
        bool Confirm();
        void Cancel();
        void SetTraySize(traySize item);
    }
}
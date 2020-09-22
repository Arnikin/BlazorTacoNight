using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SRA.Client.Models
{
    public class RemoveItemEventArgs<T> : EventArgs
    {
        public T RemovedItem
        {
            get { return removedItem; }
        }
        private T removedItem;

        public RemoveItemEventArgs(T removedItem)
        {
            this.removedItem = removedItem;
        }
    }

    public class ExperimentModelCollection: BindingList<ExperimentModel>
    {
        public event EventHandler<RemoveItemEventArgs<ExperimentModel>> RemovingItem;
        protected virtual void OnRemovingItem(RemoveItemEventArgs<ExperimentModel> args)
        {
            RemovingItem?.Invoke(this, args);
        }

        public ExperimentModelCollection()
        {
        }

        protected override void RemoveItem(int index)
        {
            OnRemovingItem(new RemoveItemEventArgs<ExperimentModel>(this[index]));
            base.RemoveItem(index);
        }

        private void ExperimentModelCollection_ListChanged(object sender, ListChangedEventArgs e)
        {

        }
    }

    public class PlateModelCollection : BindingList<PlateModel> { }

    public enum interval
    {
        [Description("Every half hour")]
        EveryHalfHour = 30,

        [Description("Every hour")]
        EveryHour = 60,

        [Description("Every two hours")]
        EveryTwoHours = 120,

        [Description("Every four hours")]
        EveryFourHours = 240,
    }

    public enum traySize
    {
        [Description("24 Wells")]
        Wells24 = 24,
        [Description("35 Wells")]
        Wells35 = 35,
        [Description("48 Wells")]
        Wells48 = 48,
        [Description("96 Wells")]
        Wells96 = 96
                }

    public enum runType
    {
        Measurement,
        Calibration
    }

    public enum runStatus
    {
        Stopped,
        Running,
        Done,
        Failed,
        Stopping,
        Starting,
        Pausing,
        Paused,
    }

    [Flags]
    public enum HardwareStatus
    {
        None = 0x0,
        ReadyToPowerOn = 0x1,
        DriversEnabled = 0x2,
        ReadyDrive1 = 0x4,
        ReadyDrive2 = 0x8,
        HasError = 0x10,
        IsMoving = 0x20,
        EnableRequest = 0x40,
        EmergencyStopActive = 0x100,
        CabinetEmergencyStopPressed = 0x200,
        ExternalEmergencyStopPressed = 0x400,
        SystemOK = 0x800,
        SecurityModuleActive = 0x1000,
        ResetButtonPressed = 0x2000,
        TemperatureHighCabinet = 0x4000,
    }


    public class PlateModel
    {
        private static string[] PlateTitles = new string[] {"A1", "B1", "C1", "D1", "A2", "B2", "C2", "D2", "A3", "B3", "C3", "D3", "A4", "B4", "C4", "D4"};

        public int Number { get; set; }
        public traySize TraySize { get; set; }
        public int NumberOfWells { get; set; }
        public float MeasurementVolume { get; set; }
        public float WaterVolume { get; set; }
        public DateTime WaterTimestamp { get; set; }
        public string Filters { get; set; }
        public int Duration { get; set; }
        public float Temperature { get; set; }
        public int Rules { get; set; }
        public string Description { get; set; }
        public string Company { get; set; }
        public string LotNumber { get; set; }
        public string Rep { get; set; }
        public string Chemicals { get; set; }
        public float ChemicalsVolume { get; set; }
        public string ReferenceNumber { get; set; }
        public string AdditionalInformation { get; set; }
        public int MeasurementSize { get; set; }
        public ExperimentModel Experiment { get; set; }

        public string Title { get
            {
                return PlateModel.PlateTitles[this.Number - 1];
            } 
        }

    }

    public class MeasurementSettings
    {
        public runType RunType { get; set; }
        public string RunName { get; set; }
        public string User { get; set; }
        public string Location { get; set; }
        public interval MeasurementInterval { get; set; }
        public float EnvironmentTemperature { get; set; }
    }

    public class ExperimentModel
    {

        public Guid Id { get; private set; }

        public ExperimentModel()
        {
            Id = Guid.NewGuid();
            MeasurementInterval = interval.EveryHalfHour;
            TraySize = traySize.Wells24;
        }

        public ExperimentModel(ExperimentModel experiment)
        {
            this.Id = experiment.Id;
            this.Name = experiment.Name;
            this.MeasurementSize = experiment.MeasurementSize;
        }

        [Required]
        [StringLength(16, ErrorMessage = "Identifier too long (16 character limit).")]
        public string Name { get; set; }

        [Required]
        [Range(1, 1000, ErrorMessage = "Size has to be in the range of 1-1000")]
        public interval MeasurementInterval { get; set; }
        public int MeasurementSize { get; set; }
        public int Duration { get; set; }

        public string User { get; set; }

        public string Location { get; set; }


        public float EnvironmentTemperature { get; set; }

        public bool MeasureTemperature { get; set; }
        public traySize TraySize { get; set; }
    }

    public static class ModelExtensions
    {
        public static ExperimentModel Copy(this ExperimentModel experiment)
        {
            return new ExperimentModel(experiment);
        }

        public static void Save(this ExperimentModel experiment, ExperimentModel dataToSave)
        {
            experiment.Name = dataToSave.Name;
            experiment.MeasurementSize = dataToSave.MeasurementSize;
        }

    }
}

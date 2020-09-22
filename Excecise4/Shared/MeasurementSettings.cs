using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Q2.DataCollector
{
    [DataContract]
    public enum interval
    {
        [Description("Every half hour")]
        [EnumMember]
        EveryHalfHour = 30,

        [Description("Every hour")]
        [EnumMember]
        EveryHour = 60,

        [Description("Every two hours")]
        [EnumMember]
        EveryTwoHours = 120,

        [Description("Every four hours")]
        [EnumMember]
        EveryFourHours = 240,
    }

    [DataContract]
    public enum traySize
    {
        [Description("24 Wells")]
        [EnumMember]
        Wells24 = 24,
        [Description("35 Wells")]
        [EnumMember]
        Wells35 = 35,
        [Description("48 Wells")]
        [EnumMember]
        Wells48 = 48,
        [Description("96 Wells")]
        [EnumMember]
        Wells96 = 96

    }

    [DataContract]
    public enum runType
    {
        [EnumMember]
        Measurement,
        [EnumMember]
        Calibration
    }

    [DataContract]
    public enum runStatus
    {
        [EnumMember]
        Stopped,
        [EnumMember]
        Running,
        [EnumMember]
        Done,
        [EnumMember]
        Failed,
        [EnumMember]
        Stopping,
        [EnumMember]
        Starting,
        [EnumMember]
        Pausing,
        [EnumMember]
        Paused,
    }

    [DataContract]
    [Flags]
    public enum HardwareStatus
    {
        [EnumMember]
        None = 0x0,
        [EnumMember]
        ReadyToPowerOn = 0x1,
        [EnumMember]
        DriversEnabled = 0x2,
        [EnumMember]
        ReadyDrive1 = 0x4,
        [EnumMember]
        ReadyDrive2 = 0x8,
        [EnumMember]
        HasError = 0x10,
        [EnumMember]
        IsMoving = 0x20,
        [EnumMember]
        EnableRequest = 0x40,
        [EnumMember]
        EmergencyStopActive = 0x100,
        [EnumMember]
        CabinetEmergencyStopPressed = 0x200,
        [EnumMember]
        ExternalEmergencyStopPressed = 0x400,
        [EnumMember]
        SystemOK = 0x800,
        [EnumMember]
        SecurityModuleActive = 0x1000,
        [EnumMember]
        ResetButtonPressed = 0x2000,
        [EnumMember]
        TemperatureHighCabinet = 0x4000,
    }


    [DataContract(Name = "Plate")]
    public class Plate
    {
        [DataMember(Order = 1, EmitDefaultValue = false, IsRequired = true)]
        public int Number { get; set; }

        [DataMember(Order = 1, EmitDefaultValue = false, IsRequired = true)]
        public traySize TraySize { get; set; }

        [DataMember(Order = 1, EmitDefaultValue = true, IsRequired = true)]
        public int NumberOfWells { get; set; }

        [DataMember(Order = 1, EmitDefaultValue = false, IsRequired = false)]
        public float MeasurementVolume { get; set; }

        [DataMember(Order = 1, EmitDefaultValue = false, IsRequired = false)]
        public float WaterVolume { get; set; }

        [DataMember(Order = 1, EmitDefaultValue = false, IsRequired = false)]
        public DateTime WaterTimestamp { get; set; }

        [DataMember(Order = 1, EmitDefaultValue = false, IsRequired = false)]
        public string Filters { get; set; }

        [DataMember(Order = 1, EmitDefaultValue = false, IsRequired = false)]
        public int Duration { get; set; }

        [DataMember(Order = 1, EmitDefaultValue = false, IsRequired = false)]
        public float Temperature { get; set; }

        [DataMember(Order = 1, EmitDefaultValue = false, IsRequired = false)]
        public int Rules { get; set; }

        [DataMember(Order = 1, EmitDefaultValue = false, IsRequired = false)]
        public string Description { get; set; }

        [DataMember(Order = 1, EmitDefaultValue = false, IsRequired = false)]
        public string Company { get; set; }

        [DataMember(Order = 1, EmitDefaultValue = false, IsRequired = false)]
        public string LotNumber { get; set; }

        [DataMember(Order = 1, EmitDefaultValue = false, IsRequired = false)]
        public string Rep { get; set; }

        [DataMember(Order = 1, EmitDefaultValue = false, IsRequired = false)]
        public string Chemicals { get; set; }

        [DataMember(Order = 1, EmitDefaultValue = false, IsRequired = false)]
        public float ChemicalsVolume { get; set; }

        [DataMember(Order = 1, EmitDefaultValue = false, IsRequired = false)]
        public string ReferenceNumber { get; set; }

        [DataMember(Order = 1, EmitDefaultValue = false, IsRequired = false)]
        public string AdditionalInformation { get; set; }

        [DataMember(Order = 1, EmitDefaultValue = false, IsRequired = false)]
        public int MeasurementSize { get; set; }


        [DataMember(Order = 2, IsRequired = false, EmitDefaultValue = false)]
        public DateTime Start { get; set; }

        [DataMember(Order = 2, IsRequired = false, EmitDefaultValue = false)]
        public DateTime End { get; set; }

    }

    [DataContract(Name = "MeasurementSettings")]
    public class MeasurementSettings
    {
        [DataMember(Order = 1, IsRequired = true)]
        public runType RunType { get; set; }

        [DataMember(Order = 1, IsRequired = true)]
        public string RunName { get; set; }

        [DataMember(Order = 1, EmitDefaultValue = false, IsRequired = false)]
        public string User { get; set; }

        [DataMember(Order = 1, EmitDefaultValue = false, IsRequired = false)]
        public string Location { get; set; }

        [DataMember(Order = 1, IsRequired = true)]
        public interval MeasurementInterval { get; set; }

        [DataMember(Order = 1, EmitDefaultValue = false, IsRequired = false)]
        public float EnvironmentTemperature { get; set; }

        [DataMember(Order = 1, IsRequired = true)]
        public Plate[] Plates { get; set; }

    }

    [DataContract(Name = "Experiment")]
    public class Experiment
    {
        [DataMember(Order = 1, IsRequired = true)]
        public string Name { get; set; }

        [DataMember(Order = 1, EmitDefaultValue = false, IsRequired = false)]
        public MeasurementSettings Settings { get; set; }
    }
}
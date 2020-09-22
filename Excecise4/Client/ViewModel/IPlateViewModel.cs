using SRA.Client.Models;
using System;
using System.Collections.Generic;

namespace SRA.Client.ViewModel
{
    public interface IPlateViewModel
    {
        PlateModelCollection Plates { get; }
        public Dictionary<int, PlateModelCollection> PlateRows { get; }
        event EventHandler PlatesChanged;
    }
}
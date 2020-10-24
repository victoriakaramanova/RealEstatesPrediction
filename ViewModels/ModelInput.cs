using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImotiPrediction.ViewModels
{
    public class ModelInput
    {
        public string Url { get; set; }

        public float Size { get; set; }

        public float Floor { get; set; }

        public float TotalFloors { get; set; }

        public string District { get; set; }

        public float Year { get; set; }

        public string Type { get; set; }

        public string BuildingType { get; set; }

        public float Price { get; set; }
    }
}



namespace ImotiPrediction.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using global::Services.Mapping;
    using ImotiPrediction.Models;
    using Microsoft.ML.Data;
    using Services.Mapping;

    public class ModelOutput
    {
        public float Score { get; set; }

        //public List<Search> LastSearches { get; set; }
    }
}

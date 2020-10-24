using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImotiPrediction.Models
{
    public class Search
    {
        public string Id { get; set; }

        //public string propertyId 

        public DateTime PredictedAt { get; set; }

        public float Size { get; set; }

        public float Floor { get; set; }

        public float TotalFloors { get; set; }

        public string District { get; set; }

        public float Year { get; set; }

        public string Type { get; set; }

        public string BuildingType { get; set; }

        public float Price { get; set; }

        //public ApplicationUser User { get; set; }

        public string UserId { get; set; }

        public float Score { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImotiPrediction.Models
{
    public class RawProperty
    {
        public RawProperty()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        public string Url { get; set; }

        public float Size { get; set; }

        public string District { get; set; }

        public string Type { get; set; }

        public float Floor { get; set; }

        public float TotalFloors { get; set; }

        public float Price { get; set; }

        public int Year { get; set; }

        public string BuildingType { get; set; }

        public DateTime CreatedAt { get; set; }

        public string Pic { get; set; }
    }
}

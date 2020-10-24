using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ImotiPrediction.ViewModels
{
    public class FormInput
    {
        [Required]
        public int Size { get; set; }

        [Required]
        public int Floor { get; set; }

        [Required]
        public int TotalFloors { get; set; }

        [Required]
        public string District { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public string BuildingType { get; set; }

        public string UserId { get; set; }
    }
}

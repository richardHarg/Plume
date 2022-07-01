using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLH.Plume.Core.Entities
{
    public sealed class Measurement
    {
        [Key]
        public string Timestamp { get; set; }
        [Required]
        public DateTimeOffset DateTimeUTC { get; set; }
        public double NO2 { get; set; }
        public double VOC { get; set; }
        public double PM01 { get; set; }
        public double PM10 { get; set; }
        public double PM25 { get; set; }
        public double NO2_AQI { get; set; }
        public double VOC_AQI { get; set; }
        public double PM01_AQI { get; set; }
        public double PM10_AQI { get; set; }
        public double PM25_AQI { get; set; }

    }
}

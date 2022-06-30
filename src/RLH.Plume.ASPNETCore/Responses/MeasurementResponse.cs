using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLH.Plume.ASPNETCore.Responses
{
    public class MeasurementResponse
    {
        public DateTimeOffset DateTimeUTC { get; set; }
        public double PM10 { get; set; }
        public double PM25 { get; set; }
    }
}

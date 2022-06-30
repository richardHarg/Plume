using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLH.Plume.ASPNETCore.Responses
{
    public class MeasurementGroupResponse
    {
        public string Interval { get; set; }
        public DateTimeOffset DateTimeFrom { get; set; }
        public DateTimeOffset DateTimeTo { get; set; }
        IEnumerable<MeasurementResponse> Measurements { get; set; }
    }
}

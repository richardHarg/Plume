using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLH.Plume.Models
{
    public sealed class ReportThresholdHours : IReportThresholdHours
    {
    
        public IEnumerable<DateTimeOffset> PM10HoursOverThreshold { get; internal set; }
        public int PM10HoursOverThresholdCount { get; internal set; }
        public IEnumerable<DateTimeOffset> PM25HoursOverThreshold { get; internal set; }
        public int PM25HoursOverThresholdCount { get; internal set; }
    }
}


using RLH.Plume.Core.Models;

namespace RLH.Plume.Models
{
    public sealed class ReportThresholdHours : IReportThresholdHours
    {
    
        public IEnumerable<DateTimeOffset> PM10HoursOverThreshold { get; internal set; }
        public int PM10HoursOverThresholdCount { get; internal set; }
        public IEnumerable<DateTimeOffset> PM25HoursOverThreshold { get; internal set; }
        public int PM25HoursOverThresholdCount { get; internal set; }

        public int PM10Threshold { get; internal set; }

        public int PM25Threshold { get; internal set; }
    }
}

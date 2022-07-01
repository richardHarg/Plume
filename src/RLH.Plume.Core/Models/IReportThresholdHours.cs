using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLH.Plume.Core.Models
{
    public interface IReportThresholdHours
    {
        public int PM10Threshold { get; }
        public IEnumerable<DateTimeOffset> PM10HoursOverThreshold { get; }
        public int PM10HoursOverThresholdCount { get; }

        public int PM25Threshold { get; }
        public IEnumerable<DateTimeOffset> PM25HoursOverThreshold { get; }
        public int PM25HoursOverThresholdCount { get; }
    }
}

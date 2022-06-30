using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLH.Plume.Models
{
    public interface IReportThresholdHours
    {
        public IEnumerable<DateTimeOffset> PM10HoursOverThreshold { get; }
        public int PM10HoursOverThresholdCount { get; }
        public IEnumerable<DateTimeOffset> PM25HoursOverThreshold { get; }
        public int PM25HoursOverThresholdCount { get; }
    }
}

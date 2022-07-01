using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLH.Plume.Core.Models
{
    public interface IReportPeakHours
    {
        public DateTimeOffset PM10HighestHour { get; }
        public double PM10HighestHourMeanValue { get; }
        public DateTimeOffset PM25HighestHour { get; }
        public double PM25HighestHourMeanValue { get; }
    }
}

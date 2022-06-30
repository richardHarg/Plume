using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLH.Plume.Models
{
    public sealed class ReportPeakHours : IReportPeakHours
    {
        public DateTimeOffset PM10HighestHour { get; internal set; }
        public double PM10HighestHourMeanValue { get; internal set; }
        public DateTimeOffset PM25HighestHour { get; internal set; }
        public double PM25HighestHourMeanValue { get; internal set; }
    }
}

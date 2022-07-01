using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLH.Plume.Core.Models
{
    public interface IDayReport
    {
        public DateTimeOffset DateTimeFrom { get; }
        public DateTimeOffset DateTimeTo { get; }
        public int TotalMeasurements { get; }
        public double MeanPM10Reading { get; }
        public double MeanPM25Reading { get; }
        public IReportPeakHours PeakHours { get; }
        public IReportThresholdHours HoursOverThreshold { get; }
        public IEnumerable<IHourReport> Hours { get; }
    }
}

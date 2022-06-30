using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLH.Plume.Models
{
    public sealed class DayReport : IDayReport
    {
        public DateTimeOffset DateTimeFrom {get; internal set;}

        public DateTimeOffset DateTimeTo
        {
            get { return DateTimeFrom.AddDays(1); }
        }

        public int TotalMeasurements { get; internal set; }
        public double MeanPM10Reading {get; internal set;}

        public double MeanPM25Reading {get; internal set;}

        public IReportPeakHours PeakHours {get; internal set;}

        public IReportThresholdHours HoursOverThreshold {get; internal set;}

        public IEnumerable<IHourReport> Hours { get; internal set; }
    }
}

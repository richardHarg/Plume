using RLH.Plume.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RLH.Plume.Extensions;

namespace RLH.Plume.Aggregates
{
    /// <summary>
    /// Represents a number of days worth of Measurements and their hourly measurements
    /// </summary>
    public class MultiDayReport
    {
        internal MultiDayReport(DateTimeOffset dateFrom,DateTimeOffset dateTo, IEnumerable<Measurement> measurements)
        {
            if (measurements is null)
            {
                throw new ArgumentNullException(nameof(measurements));
            }

            DateFrom = dateFrom;
            DateTo = dateTo;
            ProcessMeasurements(measurements);
        }

        public DateTimeOffset DateFrom { get; private set; }
        public DateTimeOffset DateTo { get; private set; }
        public IEnumerable<DailyReport> DailyReports { get; private set; }


        private void ProcessMeasurements(IEnumerable<Measurement> measurements)
        {
            // Go through all measurements passed, group into days, if that day 
            // contains values then create a new Daily report
            DailyReports = measurements.GroupByDay()
                                       .Where(x => x.Any())
                                       .Select(x => new DailyReport(x.Key, x));
        }


    }
}

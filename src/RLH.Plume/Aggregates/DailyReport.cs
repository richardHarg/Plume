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
    /// Represents a days worth of hourly measurements
    /// </summary>
    public  class DailyReport
    {
        internal DailyReport(DateTimeOffset date,IEnumerable<Measurement> measurements)
        {
            if (measurements is null)
            {
                throw new ArgumentNullException(nameof(measurements));
            }

            DateTimeOffset = date;
            ProcessMeasurements(measurements);
        }

        public DateTimeOffset DateTimeOffset { get; private set; }
        public IEnumerable<HourlyReport> HourlyMeasurements { get; private set; }



        public double MeanPM10Reading { get; private set; }
        public double MeanPM25Reading { get; private set; }

        public HourlyReport PM25HighestMeanGroup { get; private set; }
        public IReadOnlyCollection<HourlyReport> PM25GroupsOverModerateThreshold { get; private set; }
        public int PM25GroupsOverModerateThresholdCount { get; private set; } = 0;
        public HourlyReport PM10HighestMeanGroup { get; private set; }
        public IReadOnlyCollection<HourlyReport> PM10GroupsOverModerateThreshold { get; private set; }
        public int PM10GroupsOverModerateThresholdCount { get; private set; } = 0;


        private void ProcessMeasurements(IEnumerable<Measurement> measurements)
        {
            // Go through all the measurements passed, group them into 60 min intervals 
            // and IF there are reading for that period create a new hourly report
            HourlyMeasurements = measurements.GroupByInterval(TimeSpan.FromMinutes(60))
                                             .Where(x => x.Any())
                                             .Select(x => new HourlyReport(x.Key, x));

            var count = 0;
            double pm10meanValues = 0;
            double pm25meanValues = 0;
            PM25HighestMeanGroup = HourlyMeasurements.First();
            PM10HighestMeanGroup = HourlyMeasurements.First();

            var pm10GroupsOverModerateThreshold = new List<HourlyReport>();
            var pm25GroupsOverModerateThreshold = new List<HourlyReport>();

            foreach (HourlyReport timePeriod in HourlyMeasurements)
            {
                count++;

                pm10meanValues += timePeriod.MeanPM10Reading;
                pm25meanValues += timePeriod.MeanPM25Reading;



                // if this mean pm10 reading is greater than the current highest, replace
                if (timePeriod.MeanPM10Reading > PM10HighestMeanGroup.MeanPM10Reading)
                {
                    PM10HighestMeanGroup = timePeriod;
                }
                // if this mean pm10 reading is greater than the current highest, replace
                if (timePeriod.MeanPM25Reading > PM25HighestMeanGroup.MeanPM25Reading)
                {
                    PM25HighestMeanGroup = timePeriod;
                }

                // Check if this period is over the PM10 limit
                if (timePeriod.MeanPM10Reading >= 50)
                {
                    pm10GroupsOverModerateThreshold.Add(timePeriod);
                    PM10GroupsOverModerateThresholdCount++;
                }

                // Check if this period is over the PM10 limit
                if (timePeriod.MeanPM25Reading >= 35)
                {
                    pm25GroupsOverModerateThreshold.Add(timePeriod);
                    PM25GroupsOverModerateThresholdCount++;
                }
            }

            PM25GroupsOverModerateThreshold = pm25GroupsOverModerateThreshold;
            PM10GroupsOverModerateThreshold = pm10GroupsOverModerateThreshold;

            MeanPM10Reading = (pm10meanValues / count);
            MeanPM25Reading = (pm25meanValues / count);

        }


    }
}

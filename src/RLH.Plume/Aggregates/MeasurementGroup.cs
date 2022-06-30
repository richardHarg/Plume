using RLH.Plume.Entities;
using RLH.Plume.Enums;
using RLH.Plume.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLH.Plume.Aggregates
{
    public class MeasurementGroup : IMeasurementGroup
    {

        internal MeasurementGroup(DateTimeOffset dateTime,IEnumerable<Measurement> measurements, Interval interval)
        {
            DateTimeFrom = dateTime;
            GroupMeasurements = measurements.ToList();
            CalculateMeanValues();
            Interval = interval;
            DateTimeTo = dateTime.AddInterval(interval);
        }

        internal MeasurementGroup(DateTimeOffset dateFrom,DateTimeOffset dateTo, IEnumerable<Measurement> measurements)
        {
            DateTimeFrom = dateFrom;
            DateTimeTo = dateTo;
            GroupMeasurements = measurements.ToList();
            CalculateMeanValues();
            Interval = Interval.RANGE;
        }

        public IReadOnlyList<Measurement> GroupMeasurements { get; private set; }

        public IReadOnlyList<MeasurementGroup> SubGroupedMeasurements { get; private set; }
        public MeasurementGroup PM10HighestSubGroup { get; private set; }
        public MeasurementGroup PM25HighestSubGroup { get; private set; }


        public Interval Interval { get; }

        public DateTimeOffset DateTimeFrom { get; private set; }
        public DateTimeOffset DateTimeTo { get; private set; }

        public double MeanPM10Reading { get; private set; } = 0;

        public double MeanPM25Reading { get; private set; } = 0;

        public Measurement PM10HighestReading { get; private set; }

        public Measurement Pm25HighestReading { get; private set; }


        public IReadOnlyList<MeasurementGroup> SubGroupBy(Interval interval)
        {
            switch (interval)
            {
                case Interval.DAY: 
                    SubGroupedMeasurements = GroupMeasurements.GroupByDay()
                                       .Where(x => x.Any())
                                       .Select(x => new MeasurementGroup(x.Key, x,interval)).ToList();
                    break;
                case Interval.HOUR:
                    SubGroupedMeasurements = GroupMeasurements.GroupByInterval(TimeSpan.FromMinutes(60))
                                             .Where(x => x.Any())
                                             .Select(x => new MeasurementGroup(x.Key, x, interval)).ToList();
                    break;
                case Interval.TENMINS:
                    SubGroupedMeasurements = GroupMeasurements.GroupByInterval(TimeSpan.FromMinutes(10))
                                             .Where(x => x.Any())
                                             .Select(x => new MeasurementGroup(x.Key, x, interval)).ToList();
                    break;
                default: throw new ArgumentOutOfRangeException(nameof(interval));
            }
            CalculateHighestSubGroups();
            return SubGroupedMeasurements;
        }

        
        private void CalculateHighestSubGroups()
        {
            PM10HighestSubGroup = SubGroupedMeasurements.First();
            PM25HighestSubGroup = SubGroupedMeasurements.First();

            foreach (MeasurementGroup group in SubGroupedMeasurements)
            {
                if (group.MeanPM10Reading > PM10HighestSubGroup.MeanPM10Reading)
                {
                    PM10HighestSubGroup = group;

                }
                if (group.MeanPM25Reading > PM25HighestSubGroup.MeanPM25Reading)
                {
                    PM25HighestSubGroup = group;

                }
            }
        }

        private void CalculateMeanValues()
        {
            var count = 0;
            double pm10meanValues = 0;
            double pm25meanValues = 0;
            PM10HighestReading = GroupMeasurements.First();
            Pm25HighestReading = GroupMeasurements.First();

            foreach (Measurement measurement in GroupMeasurements)
            {
                count++;

                pm10meanValues += measurement.PM10;
                pm25meanValues += measurement.PM25;

                if (measurement.PM10 > PM10HighestReading.PM10)
                {
                    PM10HighestReading = measurement;

                }
                if (measurement.PM25 > Pm25HighestReading.PM25)
                {
                    Pm25HighestReading = measurement;

                }
            }
            MeanPM10Reading = (pm10meanValues / count);
            MeanPM25Reading = (pm25meanValues / count);
        }

    }
}

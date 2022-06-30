using RLH.Plume.Entities;
using RLH.Plume.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLH.Plume.Aggregates
{
    public interface IMeasurementGroup
    {
        public Interval Interval { get; }
        public DateTimeOffset DateTimeFrom { get;}
        public DateTimeOffset DateTimeTo { get; }
        public IReadOnlyList<Measurement> GroupMeasurements { get; }
        public IReadOnlyList<MeasurementGroup> SubGroupedMeasurements { get;}
        public MeasurementGroup PM10HighestSubGroup { get; }
        public MeasurementGroup PM25HighestSubGroup { get; }
        public double MeanPM10Reading { get; }
        public double MeanPM25Reading { get; }
        public Measurement PM10HighestReading { get; }
        public Measurement Pm25HighestReading { get; }

        public IReadOnlyList<MeasurementGroup> SubGroupBy(Interval interval);


    }
}

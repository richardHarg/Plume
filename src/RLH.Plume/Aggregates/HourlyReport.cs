using RLH.Plume.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLH.Plume.Aggregates
{
    /// <summary>
    /// Represents an hours worth of Measurements
    /// </summary>
    public class HourlyReport
    {
        internal HourlyReport(DateTimeOffset date,IEnumerable<Measurement> measurements)
        {
            DateTimeOffset = date;
            Measurements = measurements ?? throw new ArgumentNullException(nameof(measurements));
            ProcessMeasurements();
        }

        public DateTimeOffset DateTimeOffset { get; private set; }
        public IEnumerable<Measurement> Measurements { get; private set; }



        // Public reading values generated when passing through the measurements list
        public double MeanPM10Reading { get; private set; }
        public double MeanPM25Reading { get; private set; }
        public Measurement PM10HighestReading { get; private set; }
        public Measurement Pm25HighestReading { get; private set; }


        private void ProcessMeasurements()
        {
            var count = 0;
            double pm10meanValues = 0;
            double pm25meanValues = 0;
            var pm10Highest = Measurements.First();
            var pm25Highest = Measurements.First();

            foreach (Measurement measurement in Measurements)
            {
                count++;

                pm10meanValues += measurement.PM10;
                pm25meanValues += measurement.PM25;

                if (measurement.PM10 > pm10Highest.PM10)
                {
                    pm10Highest = measurement;

                }
                if (measurement.PM25 > pm25Highest.PM25)
                {
                    pm25Highest = measurement;

                }
            }

            PM10HighestReading = pm10Highest;
            Pm25HighestReading = pm25Highest;

            MeanPM10Reading = (pm10meanValues / count);
            MeanPM25Reading = (pm25meanValues / count);
        }

    }
}

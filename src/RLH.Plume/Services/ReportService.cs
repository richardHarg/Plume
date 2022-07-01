
using RLH.Plume.Models;
using RLH.Plume.Extensions;
using RLH.Plume.Core.Models;
using RLH.Plume.Core.Entities;
using RLH.Plume.Core.Services;

namespace RLH.Plume.Services
{
    public sealed class ReportService : IReportService
    {

        public IDayReport GenerateDailyReport(DateTime Date,IEnumerable<Measurement> measurements, int PM10HourlyThreshold = 50, int PM25HourlyThreshold = 35)
        {
            // Check if there are NO measurments passed for this day

            // **** Should I also be checking for days when the number of values falls below an acceptable threshold? 
            // **** Each measurment represents 1 min so each day should have 1440 measurements, if lower than....50% ? should be ignored? 
            if (measurements == null || measurements.Count() == 0)
            {
                return null;
            }
            // Group Measurements into their respective hours
            IEnumerable<IGrouping<DateTimeOffset,Measurement>> groups = measurements.GroupByInterval(TimeSpan.FromMinutes(60));

            // blank list of hourReports to populate
            ICollection<IHourReport> hours = new List<IHourReport>();

            // High and threshold values
            int totalMeasurements = 0;
            double totalPM25Readings = 0;
            double totalPM10Readings = 0;

            IHourReport PM10HighestHour = null;
            IHourReport PM25HighestHour = null;

            // Track peak hours over provided thresholds
            ICollection<DateTimeOffset> PM25HoursOver = new List<DateTimeOffset>();
            int PM25HoursOverCount = 0;

            ICollection<DateTimeOffset> PM10HoursOver = new List<DateTimeOffset>();
            int PM10HoursOverCount = 0;

            // Go through each group
            foreach (var grouping in groups)
            {
                // To calculate mean values start a count of measurements and tally total PM25/10 values
                var groupTotalMeasurements = 0;
                double groupTotalPM25Readings = 0;
                double groupTotalPM10Readings = 0;

                // Go through each Measurement this hour
                foreach (Measurement measurement in grouping)
                {
                    // Add this measurment values to the total
                    totalMeasurements++;
                    totalPM25Readings += measurement.PM25;
                    totalPM10Readings += measurement.PM10;

                    // Add these measurement values to the group total
                    groupTotalMeasurements++;
                    groupTotalPM25Readings += measurement.PM25;
                    groupTotalPM10Readings += measurement.PM10;
                }

                // Calculate mean values
                double groupMeanPM25 = (groupTotalPM25Readings / groupTotalMeasurements);
                double groupMeanPM10 = (groupTotalPM10Readings / groupTotalMeasurements);

                // Create a new hour report
                IHourReport newHourReport = new HourReport(grouping.Key, groupMeanPM10, groupMeanPM25);

                // If this new report has higher PM25 reading than the last highest then replace
                if (PM25HighestHour == null || newHourReport.MeanPM25Reading > PM25HighestHour.MeanPM25Reading)
                {
                    PM25HighestHour = newHourReport;
                }

                // If this new report has higher PM10 reading than the last highest then replace
                if (PM10HighestHour == null || newHourReport.MeanPM10Reading > PM10HighestHour.MeanPM10Reading)
                {
                    PM10HighestHour = newHourReport;
                }

                // If the mean PM25 value has exceeded the stated thereshold
                if (newHourReport.MeanPM25Reading > PM25HourlyThreshold)
                {
                    PM25HoursOver.Add(newHourReport.DateTimeFrom);
                    PM25HoursOverCount++;
                }

                // If the mean PM10 value has exceeded the stated thereshold
                if (newHourReport.MeanPM10Reading > PM10HourlyThreshold)
                {
                    PM10HoursOver.Add(newHourReport.DateTimeFrom);
                    PM10HoursOverCount++;
                }

                // Ensure new hour added to list
                hours.Add(newHourReport);
            }

            // Calculate total mean values for the whole day
            double totalMeanPM25 = (totalPM25Readings / totalMeasurements);
            double totalMeanPM10 = (totalPM10Readings / totalMeasurements);

            // Return a populated Day Report
            return new DayReport()
            {
                DateTimeFrom = Date,
                MeanPM10Reading = totalMeanPM10,
                MeanPM25Reading = totalMeanPM25,
                TotalMeasurements = totalMeasurements,
                PeakHours = new ReportPeakHours()
                {
                    PM10HighestHour = PM10HighestHour.DateTimeFrom,
                    PM25HighestHour = PM25HighestHour.DateTimeFrom,
                    PM10HighestHourMeanValue = PM10HighestHour.MeanPM10Reading,
                    PM25HighestHourMeanValue = PM25HighestHour.MeanPM25Reading
                },
                HoursOverThreshold = new ReportThresholdHours()
                {
                    PM10HoursOverThreshold = PM10HoursOver,
                    PM10HoursOverThresholdCount = PM10HoursOverCount,
                    PM25HoursOverThreshold = PM25HoursOver,
                    PM25HoursOverThresholdCount = PM25HoursOverCount,
                    PM10Threshold = PM10HourlyThreshold,
                    PM25Threshold = PM25HourlyThreshold
                },
                Hours = hours
            };
        }

        public IMultiDayReport GenerateMultiDayReport(DateTime startDate, DateTime endDate, IEnumerable<Measurement> measurements, int PM10HourlyThreshold = 50, int PM25HourlyThreshold = 35)
        {
            // Group the provided measurements by day
            IEnumerable<IGrouping<DateTime, Measurement>> groups = measurements.GroupByDay();

            // Create empty collection to hold the generated day reports
            ICollection<IDayReport> days = new List<IDayReport>();

            // Go through each day and generate a day report
            foreach(IGrouping<DateTime, Measurement> group in groups)
            {
                var newDay = GenerateDailyReport(group.Key, group, PM10HourlyThreshold, PM25HourlyThreshold);

                // Null returned if NO measurements passed for that day, if so skip
                if (newDay != null)
                {
                    days.Add(newDay);
                }
            }

            return new MultiDayReport()
            {
                DateTimeFrom = startDate,
                DateTimeTo = endDate,
                Days = days
            };

        }




     

    }


  
}

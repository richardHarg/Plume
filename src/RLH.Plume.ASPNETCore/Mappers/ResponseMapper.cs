using RLH.Plume.Aggregates;
using RLH.Plume.ASPNETCore.Responses;
using RLH.Plume.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLH.Plume.ASPNETCore.Mappers
{
    public static class ResponseMapper
    {
        public static MeasurementResponse ToResponse(this Measurement measurement)
        {
            return new MeasurementResponse()
            {
                DateTimeUTC = measurement.DateTimeUTC,
                PM10 = measurement.PM10,
                PM25 = measurement.PM25
            };
        }

        public static DailyReportResponse ToDailyReportResponse(this IMeasurementGroup measurementGroup, string highChart)
        {
            return new DailyReportResponse()
            {
                DateTime = measurementGroup.DateTimeFrom,
                PM10DailyMean = measurementGroup.MeanPM10Reading,
                PM25DailyMean = measurementGroup.MeanPM25Reading,
                PM10HighestHour = measurementGroup.PM10HighestSubGroup.DateTimeFrom,
                PM10HighestHourValue = measurementGroup.PM10HighestSubGroup.MeanPM10Reading,
                PM25HighestHour = measurementGroup.PM25HighestSubGroup.DateTimeFrom,
                PM25HighestHourValue = measurementGroup.PM25HighestSubGroup.MeanPM10Reading,
                HighChart = highChart
            };
           
        }



    }
}

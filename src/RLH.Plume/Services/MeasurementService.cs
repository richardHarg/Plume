using RLH.Plume.Entities;
using RLH.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RLH.Plume.Context;
using Microsoft.EntityFrameworkCore;
using RLH.Plume.Aggregates;
using RLH.Plume.Enums;
using RLH.Plume.Extensions;

namespace RLH.Plume.Services
{
    public class MeasurementService : IMeasurementService
    {
        private bool disposedValue;
        private PlumeContext _context;

        
        public MeasurementService(PlumeContext context)
        {
            _context = context;
        }

        public async Task<ResultOf<Measurement>> CreateAsync(string[] values)
        {
            // Attempt to create a new measurement object from the values provided
            var creationResult = ConvertStringArrayToMeasurement(values);

            // Check if this process threw any errors, if so return these with no further processing
            if (creationResult.Status == ResultStatus.Error)
            {
                return creationResult;
            }

            // Attempt to save the new measurement.
            var saveResult = await SaveMeasurement(creationResult.Value);

            // If this produced any errors return these 
            if (saveResult.Status == ResultStatus.Error)
            {
                return ResultOf<Measurement>.FromResult(saveResult);
            }

            // Otherwise return the new Measurement
            return ResultOf<Measurement>.Success(creationResult.Value);
        }


        public async Task<IEnumerable<Measurement>> GetMeasurements(DateTime dateFrom, DateTime dateTo)
        {
            return await _context.Measurements.Where(x => x.DateTimeUTC.Date >= dateFrom.Date && x.DateTimeUTC.Date <= dateTo.Date).ToListAsync();
        }
        public async Task<IEnumerable<Measurement>> GetMeasurements(DateTime date)
        {
            return await _context.Measurements.Where(x => x.DateTimeUTC.Date == date.Date).ToListAsync();
        }


        public async Task<ResultOf<IMeasurementGroup>> GetGroupAsync(DateTimeOffset dateFrom,DateTimeOffset dateTo)
        {
            // Locate measurements within the two date ranges provided
            var measurements = await _context.Measurements.Where(x => x.DateTimeUTC.Date >= dateFrom.Date && x.DateTimeUTC.Date <= dateTo.Date).ToListAsync();

            // If no measurements have been f
            if (measurements.Any() == false)
            {
                return ResultOf<IMeasurementGroup>.Error($"Measurement search from date: '{dateFrom}' to '{dateTo}' returned no results");
            }

            // Create a blank MeasurementGroup Object
            IMeasurementGroup group;

            // Check if these measurements are for a single day, if so create a new group and sort by hour/10 min 
            if (dateFrom.Date == dateTo.Date)
            {
                group = new MeasurementGroup(dateFrom, measurements, Interval.DAY);
                group.SubGroupBy(Interval.HOUR).ThenSubBy(Interval.TENMINS);
            }
            // Any period over a single day is set as 'range' with the from/too dates passed, measurements are grouped by day/hour
            else
            {
                group = new MeasurementGroup(dateFrom,dateTo,measurements);
                group.SubGroupBy(Interval.DAY).ThenSubBy(Interval.HOUR);
            }

            return ResultOf<IMeasurementGroup>.Success(group);

        }

        

        /// <summary>
        /// Checks if the provided string array contains values and any of the individual values
        /// are not null. If so attempts to parse a new Measurement instance.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        private ResultOf<Measurement> ConvertStringArrayToMeasurement(string[] values)
        {
            // check if this line is blank,empty OR contains ANY null or empty values
            if (values == null || values.Length == 0 || values.Any(x => string.IsNullOrWhiteSpace(x)))
            {
                return ResultOf<Measurement>.Error("Line is either empty or contain null/blank values, skipping.");
            }

            // Data to process, attempt to parse this into a new measurement object.
            try
            {
                var measurement = new Measurement()
                {
                    Timestamp = values[0],
                    DateTimeUTC = DateTimeOffset.FromUnixTimeSeconds(int.Parse(values[0])),
                    NO2 = double.Parse(values[2]),
                    VOC = double.Parse(values[3]),
                    PM10 = double.Parse(values[4]),
                    PM25 = double.Parse(values[5]),
                    NO2_AQI = double.Parse(values[6]),
                    VOC_AQI = double.Parse(values[7]),
                    PM10_AQI = double.Parse(values[8]),
                    PM25_AQI = double.Parse(values[9]),
                    PM01 = double.Parse(values[10]),
                    PM01_AQI = double.Parse(values[11])
                };

                return ResultOf<Measurement>.Success(measurement);
            }
            catch (Exception ex)
            {
                return ResultOf<Measurement>.Error(ex.Message);
            }
        }


        /// <summary>
        /// Takes the provided measurement and, if it doesnt already exist in the backing store
        /// creates a new Measurement db entry.
        /// </summary>
        /// <param name="measurement"></param>
        /// <returns></returns>
        private async Task<Result> SaveMeasurement(Measurement measurement)
        {
            // Measurement is valid, check if it already exists in the database, if it does return error
            if (await _context.Measurements.AnyAsync(x => x.Timestamp == measurement.Timestamp))
            {
                return ResultOf<Measurement>.Error($"Measurement with Timestamp '{measurement.Timestamp}' already exists, skipping.");
            }

            // Attempt to update and save the new measurement to the database
            _context.Measurements.Add(measurement);
            if (await _context.SaveChangesAsync() == 0)
            {
                return ResultOf<Measurement>.Error($"Error saving Measurement with Timestamp '{measurement.Timestamp}' to the backing store.");
            }
            return Result.Success();
        }


        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~MeasurementService()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

       
    }
}

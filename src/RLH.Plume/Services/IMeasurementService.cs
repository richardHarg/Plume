using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RLH.Plume.Aggregates;
using RLH.Plume.Entities;
using RLH.Plume.Enums;
using RLH.Results;

namespace RLH.Plume.Services
{
    public interface IMeasurementService :  IDisposable
    {
        Task<ResultOf<Measurement>> CreateAsync(string[] values);

        Task<IEnumerable<Measurement>> GetMeasurements(DateTime dateFrom, DateTime dateTo);

        Task<IEnumerable<Measurement>> GetMeasurements(DateTime date);


        Task<ResultOf<IMeasurementGroup>> GetGroupAsync(DateTimeOffset dateFrom, DateTimeOffset dateTo);

    }
}


using RLH.Plume.Core.Entities;
using RLH.Results;

namespace RLH.Plume.Core.Services
{
    public interface IMeasurementService :  IDisposable
    {
        Task<ResultOf<Measurement>> CreateAsync(string[] values);

        Task<IEnumerable<Measurement>> GetMeasurements(DateTime dateFrom, DateTime dateTo);

        Task<IEnumerable<Measurement>> GetMeasurements(DateTime date);

    }
}

using RLH.Plume.Core.Entities;
using RLH.Plume.Core.Models;

namespace RLH.Plume.Core.Services
{
    public interface IReportService
    {

        public IDayReport GenerateDailyReport(DateTime date,IEnumerable<Measurement> measurements, int PM10HourlyThreshold, int PM25HourlyThreshold);

        public IMultiDayReport GenerateMultiDayReport(DateTime startDate,DateTime endDate,IEnumerable<Measurement> measurements, int PM10HourlyThreshold, int PM25HourlyThreshold);
    }
}

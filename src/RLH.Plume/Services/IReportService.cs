using RLH.Plume.Aggregates;
using RLH.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLH.Plume.Services
{
    public interface IReportService
    {
        Task<ResultOf<MultiDayReport>> GetMultiDayReport(DateTimeOffset dateFrom, DateTimeOffset dateTo);

        Task<ResultOf<DailyReport>> GetDailyReport(DateTimeOffset date);
    }
}

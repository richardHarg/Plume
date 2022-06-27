using Microsoft.EntityFrameworkCore;
using RLH.Plume.Aggregates;
using RLH.Plume.Context;
using RLH.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLH.Plume.Services
{
    public class ReportService : IReportService
    {
        private PlumeContext _context;
        public ReportService(PlumeContext context)
        {
            _context = context;
        }

        public async Task<ResultOf<DailyReport>> GetDailyReport(DateTimeOffset date)
        {

            var measurements = await _context.Measurements.Where(x => x.DateTimeUTC.Date == date.Date).ToListAsync();

            if (measurements.Any() == false)
            {
                return ResultOf<DailyReport>.Error($"No Measurements record for date '{date}'");
            }

            return ResultOf<DailyReport>.Success(new DailyReport(date, measurements));
        }

        public async Task<ResultOf<MultiDayReport>> GetMultiDayReport(DateTimeOffset dateFrom, DateTimeOffset dateTo)
        {
            var measurements = await _context.Measurements.Where(x => x.DateTimeUTC.Date >= dateFrom.Date && x.DateTimeUTC.Date <= dateTo.Date).ToListAsync();

            if (measurements.Any() == false)
            {
                return ResultOf<MultiDayReport>.Error($"No Measurements record for dates '{dateFrom}' to '{dateTo}'");
            }

            return ResultOf<MultiDayReport>.Success(new MultiDayReport(dateFrom,dateTo, measurements));
        }
    }
}

using RLH.Plume.Entities;
using RLH.Plume.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLH.Plume.Services
{
    public interface IReportService
    {

        public IDayReport GenerateDailyReport(DateTime date,IEnumerable<Measurement> measurements, int PM10HourlyThreshold, int PM25HourlyThreshold);

        public IMultiDayReport GenerateMultiDayReport(DateTime startDate,DateTime endDate,IEnumerable<Measurement> measurements, int PM10HourlyThreshold, int PM25HourlyThreshold);
    }
}

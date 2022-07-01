using RLH.Plume.Services;
using RLH.Plume.Factories;
using RLH.Results;
using RLH.Plume.Core.Services;

namespace RLH.Plume.Tests
{
    public class ReportTests
    {
        private IMeasurementService _service;

        public ReportTests()
        {
            _service = new MeasurementServiceFactory().GetMeasurementService();
        }


        [Theory]
        [InlineData("2022-05-01 00:00:00.0000000", "2022-05-05 00:00:00.0000000")]
        [InlineData("2022-06-01 00:00:00.0000000", "2022-06-05 00:00:00.0000000")]
        public async void New_Report_Generation_Multi_Day(DateTime dateFrom, DateTime dateTo)
        {
            var measurements = await _service.GetMeasurements(dateFrom, dateTo);

            var report = new ReportService().GenerateMultiDayReport(dateFrom,dateTo,measurements);

            Assert.NotNull(report);
        }
        [Theory]
        [InlineData("2022-06-01 00:00:00.0000000")]
        [InlineData("2022-06-10 00:00:00.0000000")]
        public async void New_Report_Generation_Single_Day(DateTime date)
        {
            var measurements = await _service.GetMeasurements(date);

            var report = new ReportService().GenerateDailyReport(date, measurements);

            Assert.NotNull(report);
        }
        

       

    }
}
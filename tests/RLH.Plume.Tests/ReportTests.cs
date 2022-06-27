using RLH.Plume.Services;
using RLH.Plume.Factories;
using RLH.Results;

namespace RLH.Plume.Tests
{
    public class ReportTests
    {
        private IReportService _reportService;

        public ReportTests()
        {
            _reportService = new ReportServiceFactory().GetReportService();
        }


        [Fact]
        public async void Known_Good_Day_Returns_Data()
        {
            var result = await _reportService.GetDailyReport(DateTimeOffset.Parse("2022-06-02 00:00:00.0000000 +00:00"));

            Assert.Equal(ResultStatus.Success, result.Status);

            Assert.NotEmpty(result.Value.HourlyMeasurements);


        }
    }
}
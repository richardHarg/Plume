using RLH.Plume.Core.Models;

namespace RLH.Plume.HighCharts.Services
{
    public interface IHighChartService
    {
        public string GetDayChart(IDayReport dayReport);

        public string GetMultiDayChart(IMultiDayReport multiDayReport);
    }
}

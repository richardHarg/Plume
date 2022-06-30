using RLH.Plume.Aggregates;
using RLH.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLH.Plume.HighCharts.Services
{
    public interface IHighChartService
    {
        public string GetChart(IMeasurementGroup group);
    }
}

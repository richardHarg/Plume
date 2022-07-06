using Highsoft.Web.Mvc.Charts;
using Highsoft.Web.Mvc.Charts.Rendering;
using RLH.Plume.Core.Models;

namespace RLH.Plume.HighCharts.Services
{
    public class HighChartService : IHighChartService
    {
        public string GetDayChart(IDayReport dayReport)
        {
            Highcharts options = GetNewChart("DAILY", $"Daily Report: {dayReport.DateTimeFrom.ToString("ddd dd MMM yyyy")}", $"Measurement Average: Hourly", "µg/m3");

            // Add the thresholds to the chart 
            AddPMIndicatorLines(options,dayReport.HoursOverThreshold.PM10Threshold,dayReport.HoursOverThreshold.PM25Threshold);

            // Create the x axis labels with the hour/min shown
            AddXAxisLabels(options, dayReport.Hours.Select(x => x.DateTimeFrom.ToString("HH:mm")));

            // Add the data to the chart
            AddLineSeries(options, "PM 2.5", dayReport.Hours.Select(x => x.MeanPM25Reading));
            AddLineSeries(options, "PM 10", dayReport.Hours.Select(x => x.MeanPM10Reading));

            return new HighchartsRenderer(options).RenderHtml();
        }
        public string GetMultiDayChart(IMultiDayReport multiDayReport)
        {
            Highcharts options = GetNewChart("DATERANGE", $"Date Range Report: {multiDayReport.DateTimeFrom.ToString("ddd dd MMM yyyy")} - {multiDayReport.DateTimeTo.ToString("ddd dd MMM yyyy")}", $"Measurement Average: Daily", "µg/m3");

            // Create the x axis labels with the days showing
            AddXAxisLabels(options, multiDayReport.Days.Select(x => x.DateTimeFrom.ToString("ddd dd MMM")));

            // Add the data to the chart
            AddLineSeries(options, "PM 2.5", multiDayReport.Days.Select(x => x.MeanPM25Reading));
            AddLineSeries(options, "PM 10", multiDayReport.Days.Select(x => x.MeanPM10Reading));

            return new HighchartsRenderer(options).RenderHtml();
        }

        private Highcharts GetNewChart(string name, string title, string subTitle, string yTitle)
        {
            return new Highcharts()
            {

                ID = name,

                Title = new Title()
                {
                    Text = title
                },
                Subtitle = new Subtitle()
                {
                    Text = subTitle
                }
    ,
                Tooltip = new Tooltip
                {
                    HeaderFormat = "<span style='font-size:10px'>{point.key}</span><table style='font-size:12px'>",
                    PointFormat = "<tr><td style='color:{series.color};padding:0'>{series.name}: </td><td style='padding:0'><b>{point.y:.1f} µg/m3</b></td></tr>",
                    FooterFormat = "</table>",
                    Shared = true,
                    UseHTML = true
                },
                PlotOptions = new PlotOptions(),
                XAxis = new List<XAxis>(),
                YAxis = new List<YAxis>()
                {
                    new YAxis()
                    {
                         Min = 0,
                         Title = new YAxisTitle
                         {
                            Text = yTitle
                         },
                         PlotLines = new List<YAxisPlotLines>()
                    }
                },

                Series = new List<Series>()
            };
        }
        private void AddLineSeries(Highcharts chartOptions, string name, IEnumerable<double> values, PlotOptionsLine lineOptions = null)
        {
            chartOptions.PlotOptions.Line = lineOptions == null ? new PlotOptionsLine() : lineOptions;
            chartOptions.Series.Add(new LineSeries()
            {
                Name = name,
                Data = values.Select(x => new LineSeriesData() { Y = x }).ToList()

            });
        }
        private void AddXAxisLabels(Highcharts chartOptions, IEnumerable<string> xValues)
        {
            chartOptions.XAxis.Add(new XAxis
            {
                Categories = xValues.ToList()
            });
        }
        private void AddPMIndicatorLines(Highcharts chartOptions,int pm10Threshold,int pm25Threshold)
        {
            chartOptions.YAxis.First().PlotLines = new List<YAxisPlotLines>()
             {
                 new YAxisPlotLines()
                            {
                                Color = "Yellow",
                                DashStyle = YAxisPlotLinesDashStyle.Dash,
                                Width = 1,
                                Label = new YAxisPlotLinesLabel()
                                {
                                    Text = "PM 2.5 - Moderate"
                                },
                                Value = pm25Threshold
                            },
                            new YAxisPlotLines()
                            {
                                Color = "Yellow",
                                DashStyle = YAxisPlotLinesDashStyle.Dash,
                                Width = 1,
                                Label = new YAxisPlotLinesLabel()
                                {
                                    Text = "PM 10 - Moderate / PM 2.5 High"
                                },
                                Value = pm10Threshold
                            }
                            /*,

                            new YAxisPlotLines()
                            {
                                Color = "Red",
                                DashStyle = YAxisPlotLinesDashStyle.Dash,
                                Width = 1,
                                Label = new YAxisPlotLinesLabel()
                                {
                                    Text = "PM 10 - High",
                                },
                                Value = 76

                            }
                            */
             };

        }
    }
}

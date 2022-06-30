using Highsoft.Web.Mvc.Charts;
using Highsoft.Web.Mvc.Charts.Rendering;
using RLH.Plume.Aggregates;
using RLH.Plume.Enums;

namespace RLH.Plume.HighCharts.Services
{
    public class HighChartService : IHighChartService
    {
        public string GetChart(IMeasurementGroup group)
        {
            Highcharts options;

            switch (group.Interval)
            {
                case Interval.RANGE:
                    options = GetNewChart("DATE RANGE", $"Date Range Report: {group.DateTimeFrom.ToString("ddd dd MMM yyyy")} - {group.DateTimeTo.ToString("ddd dd MMM yyyy")}", $"Measurement Average: Daily", "µg/m3");
                    AddXAxisLabels(options, group.SubGroupedMeasurements.Select(x => x.DateTimeFrom.ToString("ddd dd MMM")));
                    break;
                case Interval.DAY:
                    options = GetNewChart("DAILY", $"Daily Report: {group.DateTimeFrom.ToString("ddd dd MMM yyyy")}", $"Measurement Average: Hourly", "µg/m3");
                    AddPMIndicatorLines(options); // ensure the threshhold lines are added to the graph
                    AddXAxisLabels(options, group.SubGroupedMeasurements.Select(x => x.DateTimeFrom.ToString("HH:mm")));
                    break;
                default : throw new ArgumentOutOfRangeException(nameof(group.Interval));
            }

            AddLineSeries(options,"PM 2.5", group.SubGroupedMeasurements.Select(x => x.MeanPM25Reading));
            AddLineSeries(options, "PM 10", group.SubGroupedMeasurements.Select(x => x.MeanPM10Reading));

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
        private void AddPMIndicatorLines(Highcharts chartOptions)
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
                                Value = 35
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
                                Value = 50
                            },

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
             };

        }
    }
}

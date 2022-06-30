using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLH.Plume.Models
{
    public sealed class HourReport : IHourReport
    {
        internal HourReport(DateTimeOffset dateTimeFrom, double meanPM10Reading, double meanPM25Reading)
        {
            DateTimeFrom = dateTimeFrom;
            DateTimeTo = dateTimeFrom.AddHours(1);
            MeanPM10Reading = meanPM10Reading;
            MeanPM25Reading = meanPM25Reading;
        }

        public DateTimeOffset DateTimeFrom {get; private set;}

        public DateTimeOffset DateTimeTo { get; private set;}

        public double MeanPM10Reading { get; private set;}

        public double MeanPM25Reading { get; private set;}
    }
}

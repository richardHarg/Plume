using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLH.Plume.ASPNETCore.Responses
{
    public class DailyReportResponse
    {
        public DateTimeOffset DateTime { get; set; }

        public double PM25DailyMean { get; set; }

        public double PM10DailyMean { get; set; }

        public DateTimeOffset PM25HighestHour { get; set; }

        public double PM25HighestHourValue { get; set; }

        public DateTimeOffset PM10HighestHour { get; set; }

        public double PM10HighestHourValue { get; set; }

        public string HighChart { get; set; }
    }
}

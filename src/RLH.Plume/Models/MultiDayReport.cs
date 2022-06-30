using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLH.Plume.Models
{
    public sealed class MultiDayReport : IMultiDayReport
    {
        public DateTimeOffset DateTimeFrom { get; internal set; }

        public DateTimeOffset DateTimeTo { get; internal set; }

        public IEnumerable<IDayReport> Days { get; internal set; }
    }
}

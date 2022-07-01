
using RLH.Plume.Core.Models;

namespace RLH.Plume.Models
{
    public sealed class MultiDayReport : IMultiDayReport
    {
        public DateTimeOffset DateTimeFrom { get; internal set; }

        public DateTimeOffset DateTimeTo { get; internal set; }

        public IEnumerable<IDayReport> Days { get; internal set; }
    }
}

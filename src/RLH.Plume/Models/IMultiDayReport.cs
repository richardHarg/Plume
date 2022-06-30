using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLH.Plume.Models
{
    public interface IMultiDayReport
    {
        public DateTimeOffset DateTimeFrom { get; }
        public DateTimeOffset DateTimeTo { get; }
        public IEnumerable<IDayReport> Days { get; }
    }
}

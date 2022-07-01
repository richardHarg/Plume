using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLH.Plume.Core.Models
{
    public interface IHourReport
    {
        public DateTimeOffset DateTimeFrom { get; }
        public DateTimeOffset DateTimeTo { get; }
        public double MeanPM10Reading { get; }
        public double MeanPM25Reading { get; }
    }
}

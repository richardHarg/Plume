using RLH.Plume.Aggregates;
using RLH.Plume.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLH.Plume.Extensions
{
    internal static class Extensions
    {
        public static void ThenSubBy(this IReadOnlyList<MeasurementGroup> measurementGroups,Interval interval)
        {
            if (measurementGroups is null)
            {
                throw new ArgumentNullException(nameof(measurementGroups));
            }

            foreach(var measurementGroup in measurementGroups)
            {
                measurementGroup.SubGroupBy(interval);
            }
        }




        internal static DateTimeOffset AddInterval(this DateTimeOffset start, Interval interval)
        {
            switch (interval)
            {
                case Interval.DAY:
                    return start.AddDays(1);
                    break;
                case Interval.HOUR:
                    return start.AddHours(1);
                    break;
                case Interval.TENMINS:
                    return start.AddMinutes(10);
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}

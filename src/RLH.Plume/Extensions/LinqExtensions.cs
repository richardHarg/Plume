
using RLH.Plume.Core.Entities;

namespace RLH.Plume.Extensions
{
    public static class LinqExtensions
    {
        public static IEnumerable<IGrouping<DateTimeOffset, Measurement>> GroupByInterval(this IEnumerable<Measurement> collection, TimeSpan timeSpan)
        {
            return collection.GroupBy(x =>
            {
                var currentTime = x.DateTimeUTC;
                currentTime = currentTime.AddMinutes(-(currentTime.Minute % timeSpan.TotalMinutes));
                currentTime = currentTime.AddMilliseconds(-currentTime.Millisecond - 1000 * currentTime.Second);
                return currentTime;
            });
        }

        public static IEnumerable<IGrouping<DateTime, Measurement>> GroupByDay(this IEnumerable<Measurement> collection)
        {
            return collection.GroupBy(x => x.DateTimeUTC.Date);

        }
    }
}

namespace Playground.API.Timezone
{
    public class GetDateTimeOffsetQuery
    {
        public string TimeZone { get; }

        public GetDateTimeOffsetQuery(string timeZone)
        {
            TimeZone = timeZone;
        }
    }
    public class TimeZoneQueryHandler
    {
        private static readonly Dictionary<string, string> TimeZones = new Dictionary<string, string>
    {
        { "ICT", "SE Asia Standard Time" },
        { "GMT", "GMT Standard Time" },
        { "PDT", "Pacific Daylight Time" }
    };

        public DateTimeOffset Handle(GetDateTimeOffsetQuery query)
        {
            if (!TimeZones.TryGetValue(query.TimeZone.ToUpper(), out string timeZoneId))
            {
                throw new ArgumentException("Invalid time zone");
            }

            TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
            DateTimeOffset dateTimeOffset = TimeZoneInfo.ConvertTime(DateTimeOffset.UtcNow, timeZoneInfo);
            return dateTimeOffset;
        }
    }
}

using SharedKernel;

namespace Application.Organisations.GetByLaestab;

public class StatusCalculator(IDateTimeProvider dateTimeProvider)
{
    private readonly TimeSpan _startOfDay = new(8, 0, 0);
    private readonly TimeSpan _endOfDay = new(15, 30, 0);
    private static readonly TimeZoneInfo BritishTimeZone = TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time");
    
    public OrgStatus GetOpenStatus()
    {
        DateTime utcNow = dateTimeProvider.UtcNow;
        DateTime britishLocalTime = TimeZoneInfo.ConvertTimeFromUtc(utcNow, BritishTimeZone);
        TimeSpan timeOfDay = britishLocalTime.TimeOfDay;
        
        return timeOfDay >= _startOfDay
               && timeOfDay < _endOfDay
            ? OrgStatus.Open
            : OrgStatus.Closed;
    }
}

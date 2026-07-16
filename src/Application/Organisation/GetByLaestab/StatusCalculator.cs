using SharedKernel;

namespace Application.Organisation.GetByLaestab;

public class StatusCalculator(IDateTimeProvider dateTimeProvider)
{
    private readonly TimeSpan _startOfDay = new(8, 0, 0);
    private readonly TimeSpan _endOfDay = new(15, 30, 0);
    
    public OrgStatus GetOpenStatus()
    {
        TimeSpan timeOfDay = dateTimeProvider.UtcNow.TimeOfDay;
        
        return timeOfDay >= _startOfDay
               && timeOfDay <= _endOfDay
            ? OrgStatus.Open
            : OrgStatus.Closed;
    }
}

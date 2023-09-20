using NGOT.Common.Interfaces;

namespace NGOT.Infrastructure.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime Now => DateTimeOffset.Now.DateTime;
}
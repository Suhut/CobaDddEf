using DddEf.Application.Common.Abstractions;

namespace DddEf.Infrastructure.Services;

internal class DateTimeProvider : IDateTimeProvider
{
    public DateTimeOffset Now => DateTimeOffset.Now;
}

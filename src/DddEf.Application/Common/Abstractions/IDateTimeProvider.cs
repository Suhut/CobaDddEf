namespace DddEf.Application.Common.Abstractions;

public interface IDateTimeProvider
{
    DateTimeOffset Now { get; }
}

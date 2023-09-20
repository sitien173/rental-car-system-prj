namespace NGOT.Common.Interfaces;

public interface IDateTimeProvider : ISingleton
{
    public DateTime Now { get; }
}
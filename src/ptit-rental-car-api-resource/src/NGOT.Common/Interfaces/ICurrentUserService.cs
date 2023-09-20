namespace NGOT.Common.Interfaces;

public interface ICurrentUserService : IScoped
{
    string? UserId { get; }
}
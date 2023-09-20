namespace NGOT.Common.Interfaces;

public interface IGuidGenerator : IScoped
{
    Guid Create();
}
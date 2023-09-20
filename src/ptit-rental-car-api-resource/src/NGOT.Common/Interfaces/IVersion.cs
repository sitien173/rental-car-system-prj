namespace NGOT.Common.Interfaces;

public interface IVersion
{
    byte[] RowVersion { get; set; }
}
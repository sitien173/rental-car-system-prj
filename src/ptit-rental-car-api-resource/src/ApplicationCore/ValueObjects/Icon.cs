using Microsoft.EntityFrameworkCore;
using NGOT.Common.Models;

namespace NGOT.ApplicationCore.ValueObjects;

[Owned]
public class Icon : ValueObject<Icon>
{
    public string Host { get; set; } = null!;
    public string FileName { get; set; } = null!;
    public int Size { get; set; }
    public string Type { get; set; } = null!;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Host;
        yield return FileName;
        yield return Size;
        yield return Type;
    }
}
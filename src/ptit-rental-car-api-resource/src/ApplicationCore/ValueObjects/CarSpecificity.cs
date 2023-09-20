using Microsoft.EntityFrameworkCore;
using NGOT.Common.Models;

namespace NGOT.ApplicationCore.ValueObjects;

[Owned]
public class CarSpecificity : ValueObject<CarSpecificity>
{
    public string Transmission { get; set; } = null!;
    public string Fuel { get; set; } = null!;
    public int Seat { get; set; }
    public string FuelConsumption { get; set; } = null!;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Transmission;
        yield return Fuel;
        yield return Seat;
        yield return FuelConsumption;
    }
}
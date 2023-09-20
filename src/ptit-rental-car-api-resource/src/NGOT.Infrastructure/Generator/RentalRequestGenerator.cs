using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace NGOT.Infrastructure.Generator;

public class RentalRequestGenerator : ValueGenerator<string>
{
    private const string _prefix = "Rq";

    [ThreadStatic] private static int _counter;

    private static DateTime _lastGeneratedDate;

    static RentalRequestGenerator()
    {
        _counter = 1;
        _lastGeneratedDate = DateTime.MinValue;
    }

    public override bool GeneratesTemporaryValues => false;

    public override string Next(EntityEntry entry)
    {
        var today = DateTime.Today;

        if (today > _lastGeneratedDate)
        {
            // Reset the counter for a new day
            _counter = 1;
            _lastGeneratedDate = today;
        }

        // Increment the counter for the next call
        var currentCounter = _counter++;

        return $"{_prefix}-{DateTime.Today:yyyyMMdd}{currentCounter:D6}";
    }
}
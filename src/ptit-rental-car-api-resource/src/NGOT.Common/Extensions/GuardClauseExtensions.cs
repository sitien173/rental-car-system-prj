using Ardalis.GuardClauses;

namespace NGOT.Common.Extensions;

public static class GuardClauseExtensions
{
    public static void InValidOperation(this IGuardClause guardClause, bool condition, string message)
    {
        if (condition) throw new InvalidOperationException(message);
    }

    public static void False(this IGuardClause guardClause, bool condition, string paramName, string message)
    {
        if (condition)
            throw new ArgumentException(message, paramName);
    }
}
namespace NGOT.Common.Models;

public class Page<T>
{
    public Page(IReadOnlyList<T> data, int pageIndex, int limit, int total)
    {
        Data = data;
        PageIndex = pageIndex;
        Limit = limit;
        Total = total;
    }

    public IReadOnlyList<T> Data { get; }
    public int PageIndex { get; }
    public int Limit { get; }
    public int Total { get; }
}
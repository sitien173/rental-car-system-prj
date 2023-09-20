namespace NGOT.API.ErrorHandlers;

public class Error
{
    public string? Type { get; set; }
    public string? Title { get; set; }
    public int? Status { get; set; }
    public string? Detail { get; set; }
    public string? Instance { get; set; }
    public string? TraceId { get; set; }
    public Dictionary<string, IEnumerable<string>> Errors { get; set; } = new ();
    public DateTime Timestamp { get; set; } = DateTime.Now;
}
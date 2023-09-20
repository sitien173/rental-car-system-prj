namespace NGOT.Common.Models;

public class UploadFileResponse
{
    public string Host { get; set; } = null!;
    public string FileName { get; set; } = null!;
    public string? Thumbnail { get; set; }
    public int Size { get; set; }
    public string Type { get; set; } = null!;
}
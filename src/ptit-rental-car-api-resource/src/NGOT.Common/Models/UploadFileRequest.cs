namespace NGOT.Common.Models;

public record UploadFileRequest(string FileName)
{
    public byte[] File { get; set; } = Array.Empty<byte>();
    public bool ShouldCreateThumbnail { get; set; }
}
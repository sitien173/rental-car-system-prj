using NGOT.Common.Models;

namespace NGOT.Common.Interfaces;

public interface IFileProvider : ITransient
{
    Task<UploadFileResponse> UploadFileAsync(UploadFileRequest request, CancellationToken cancellationToken = default);
    Task DeleteFileAsync(string filename, CancellationToken cancellationToken = default);
    Task<IEnumerable<string>> GetAllFilesAsync();
}
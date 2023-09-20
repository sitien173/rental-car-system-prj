using AutoMapper;
using NGOT.Common.Models;

namespace NGOT.API.Converters;

public sealed class FormFileToUploadFileRequestConverter : ITypeConverter<IFormFile, UploadFileRequest>
{
    public UploadFileRequest Convert(IFormFile source, UploadFileRequest destination, ResolutionContext context)
    {
        using var memoryStream = new MemoryStream();
        source.CopyTo(memoryStream);

        return new UploadFileRequest(source.FileName)
        {
            File = memoryStream.ToArray()
        };
    }
}
using NGOT.Common.Interfaces;
using NGOT.Common.Models;
using NGOT.Common.Settings;
using Serilog;
using SkiaSharp;
using ILogger = Serilog.ILogger;

namespace NGOT.API.Services;

public class FileProviderDevelopment : IFileProvider
{
    private const string _uploadsFolder = "uploads";
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IWebHostEnvironment _env;
    private readonly FileUploadSettings _fileUploadSettings;
    private readonly ILogger _logger = Log.ForContext<FileProviderDevelopment>();

    public FileProviderDevelopment(IWebHostEnvironment env, IDateTimeProvider dateTimeProvider,
        IHttpContextAccessor contextAccessor, FileUploadSettings fileUploadSettings)
    {
        _env = env;
        _dateTimeProvider = dateTimeProvider;
        _contextAccessor = contextAccessor;
        _fileUploadSettings = fileUploadSettings;
    }

    public async Task<UploadFileResponse> UploadFileAsync(UploadFileRequest request,
        CancellationToken cancellationToken = default)
    {
        var response = new UploadFileResponse();

        var fileTime = _dateTimeProvider.Now.ToFileTime();
        var fileExtension = Path.GetExtension(request.FileName);
        response.Type = fileExtension;
        response.Size = request.File.Length;

        var fileName = Path.GetFileNameWithoutExtension(request.FileName);

        var req = _contextAccessor.HttpContext?.Request;
        var hostUrl = req?.Scheme + "://" + req?.Host.Value;

        var uniqueFileName = $"{fileName}-{fileTime}{fileExtension}";
        response.FileName = $"{uniqueFileName}";
        response.Host = $"{hostUrl}{Path.DirectorySeparatorChar}{_uploadsFolder}{Path.DirectorySeparatorChar}";

        using var stream = new MemoryStream(request.File);
        _logger.Information("UploadFileAsync: {FileName}", request.FileName);

        if (!Directory.Exists(Path.Combine(_env.WebRootPath, _uploadsFolder)))
            Directory.CreateDirectory(Path.Combine(_env.WebRootPath, _uploadsFolder));

        var filePath = Path.Combine(_env.WebRootPath, _uploadsFolder, uniqueFileName);

        await using var fileStream = new FileStream(filePath, FileMode.Create);
        await stream.CopyToAsync(fileStream, cancellationToken);

        if (request is { ShouldCreateThumbnail: true })
        {
            var thumbnail = ResizeImage(request.File);

            using var thumbnailStream = new MemoryStream(thumbnail);
            _logger.Information("UploadFileAsync: {FileName}", request.FileName);

            var thumbnailFileName = $"thumbnail-{fileName}-{fileTime}{fileExtension}";

            var thumbnailFilePath = Path.Combine(_env.WebRootPath, _uploadsFolder, thumbnailFileName);

            await using var thumbnailFileStream = new FileStream(thumbnailFilePath, FileMode.Create);
            await thumbnailStream.CopyToAsync(thumbnailFileStream, cancellationToken);
            response.Thumbnail = thumbnailFileName;
        }

        return response;
    }

    public Task DeleteFileAsync(string filename, CancellationToken cancellationToken = default)
    {
        var filePath = Path.Combine(_env.WebRootPath, _uploadsFolder, filename);
        if (File.Exists(filePath)) File.Delete(filePath);
        return Task.CompletedTask;
    }

    public async Task<IEnumerable<string>> GetAllFilesAsync()
    {
        var filePath = Path.Combine(_env.WebRootPath, _uploadsFolder);
        return await Task.FromResult(Directory.GetFiles(filePath));
    }

    private byte[] ResizeImage(byte[] file)
    {
        using var inputStream = new MemoryStream(file);
        using var outputStream = new MemoryStream();
        using var original = SKBitmap.Decode(inputStream);
        using var resized = original.Resize(new SKImageInfo(_fileUploadSettings.ThumbnailConfigs.ImageConfigs.Width,
            _fileUploadSettings.ThumbnailConfigs.ImageConfigs.Height), SKFilterQuality.High);
        using var image = SKImage.FromBitmap(resized);
        using var data = image.Encode(SKEncodedImageFormat.Jpeg,
            _fileUploadSettings.ThumbnailConfigs.ImageConfigs.Quality);
        data.SaveTo(outputStream);
        return outputStream.ToArray();
    }
}
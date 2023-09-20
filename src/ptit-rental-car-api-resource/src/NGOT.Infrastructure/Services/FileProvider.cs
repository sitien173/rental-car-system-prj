using Google.Cloud.Storage.V1;
using NGOT.Common.Interfaces;
using NGOT.Common.Models;
using NGOT.Common.Settings;
using Serilog;
using SkiaSharp;

namespace NGOT.Infrastructure.Services;

public class FileProvider : IFileProvider
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly FileUploadSettings _fileUploadSettings;
    private readonly ILogger _logger = Log.ForContext<FileProvider>();
    private readonly StorageClient _storageClient;
    private readonly GoogleCloudStorageSettings _storageSettings;

    public FileProvider(GoogleCloudStorageSettings storageSettings, StorageClient storageClient,
        FileUploadSettings fileUploadSettings, IDateTimeProvider dateTimeProvider)
    {
        _storageSettings = storageSettings;
        _storageClient = storageClient;
        _fileUploadSettings = fileUploadSettings;
        _dateTimeProvider = dateTimeProvider;
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

        var uniqueFileName = $"{fileName}-{fileTime}{fileExtension}";
        response.FileName = $"{uniqueFileName}";
        response.Host = Path.Combine(_storageSettings.BaseUri, _storageSettings.BucketName) +
                        Path.DirectorySeparatorChar;

        using var stream = new MemoryStream(request.File);
        _logger.Information("UploadFileAsync: {FileName}", request.FileName);

        await _storageClient.UploadObjectAsync(_storageSettings.BucketName, uniqueFileName, null, stream,
            cancellationToken: cancellationToken);

        if (request is { ShouldCreateThumbnail: true })
        {
            var thumbnail = ResizeImage(request.File, _fileUploadSettings.ThumbnailConfigs.ImageConfigs.Width,
                _fileUploadSettings.ThumbnailConfigs.ImageConfigs.Height);

            using var thumbnailStream = new MemoryStream(thumbnail);
            _logger.Information("UploadFileAsync: {FileName}", request.FileName);

            var thumbnailFileName = $"thumbnail-{fileName}-{fileTime}{fileExtension}";

            await _storageClient.UploadObjectAsync(_storageSettings.BucketName, thumbnailFileName, null,
                thumbnailStream, cancellationToken: cancellationToken);

            response.Thumbnail = thumbnailFileName;
        }

        return response;
    }

    public Task DeleteFileAsync(string filename, CancellationToken cancellationToken = default)
    {
        _logger.Information("DeleteFileAsync: {FileUrl}", filename);
        return _storageClient.DeleteObjectAsync(_storageSettings.BucketName, filename,
            cancellationToken: cancellationToken);
    }

    public async Task<IEnumerable<string>> GetAllFilesAsync()
    {
        var files = new List<string>();
        var storageObjects = _storageClient.ListObjectsAsync(_storageSettings.BucketName,
            options: new ListObjectsOptions
            {
                Fields = "items(name)"
            });
        await foreach (var storageObject in storageObjects) files.Add(storageObject.Name);
        return files;
    }

    private byte[] ResizeImage(byte[] file, int width, int height)
    {
        using var inputStream = new MemoryStream(file);
        using var outputStream = new MemoryStream();
        using var original = SKBitmap.Decode(inputStream);
        using var resized = original.Resize(new SKImageInfo(width, height), SKFilterQuality.High);
        using var image = SKImage.FromBitmap(resized);
        using var data = image.Encode(SKEncodedImageFormat.Jpeg,
            _fileUploadSettings.ThumbnailConfigs.ImageConfigs.Quality);
        data.SaveTo(outputStream);
        return outputStream.ToArray();
    }
}
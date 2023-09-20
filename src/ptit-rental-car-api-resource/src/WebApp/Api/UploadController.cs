using Microsoft.AspNetCore.Mvc;
using NGOT.Common.Extensions;
using NGOT.Common.Interfaces;
using NGOT.Common.Models;

namespace NGOT.API.Api;

[ApiController]
[ApiVersion("1.0")]
[ApiExplorerSettings(IgnoreApi = true)]
[Route("api/v{version:apiVersion}/upload")]
public class UploadController : BaseController
{
    private readonly IFileProvider _fileProvider;

    public UploadController(IFileProvider fileProvider)
    {
        _fileProvider = fileProvider;
    }

    [HttpPost]
    [Produces(typeof(UploadFileResponse[]))]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Upload(CancellationToken ct)
    {
        var files = Request.Form.Files;
        var uploadFileRequests = Mapper.Map<UploadFileRequest[]>(files);
        uploadFileRequests.ForEach(x => x.ShouldCreateThumbnail = Request.Form["shouldCreateThumbnail"] == "true");

        var uploadFileResponses = new List<UploadFileResponse>(uploadFileRequests.Length);
        foreach (var item in uploadFileRequests)
        {
            var uploadFileResponse = await _fileProvider.UploadFileAsync(item, ct);
            uploadFileResponses.Add(uploadFileResponse);
        }

        return Ok(uploadFileResponses);
    }

    [HttpPost("delete")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Delete(CancellationToken ct)
    {
        var removeFileName = Request.Form["filename"].ToString();
        await _fileProvider.DeleteFileAsync(removeFileName, ct);
        return NoContent();
    }
}
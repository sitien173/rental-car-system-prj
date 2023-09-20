using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NGOT.ApplicationCore.Dto.Feature;
using NGOT.ApplicationCore.Dto.RentalDocuments;
using NGOT.ApplicationCore.Interfaces;

namespace NGOT.API.Api;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/rental-documents")]
[Authorize]
public class RentalDocumentsController : BaseController
{
    private readonly IRentalDocumentsService _rentalDocumentService;

    public RentalDocumentsController(IRentalDocumentsService rentalDocumentService)
    {
        _rentalDocumentService = rentalDocumentService;
    }

    [HttpGet]
    [Produces(typeof(FeatureResponse[]))]
    public async Task<IActionResult> Get(CancellationToken ct)
    {
        var features = await _rentalDocumentService.GetAllAsync(ct);
        return Ok(features);
    }

    [HttpGet("{id:guid}")]
    [Produces(typeof(FeatureResponse))]
    public async Task<IActionResult> Get(Guid id, CancellationToken ct)
    {
        var feature = await _rentalDocumentService.GetByIdAsync(id, ct);
        return Ok(feature);
    }

    [HttpPost]
    [Produces(typeof(RentalDocumentsResponse))]
    public async Task<IActionResult> Create(CreateRentalDocumentsRequest request, CancellationToken ct)
    {
        var id = await _rentalDocumentService.CreateAsync(request, ct);

        return Ok(id);
    }

    [HttpPut]
    [HttpPatch]
    [Route("{id:guid}")]
    [Produces(typeof(RentalDocumentsResponse))]
    public async Task<IActionResult> Update(Guid id, UpdateRentalDocumentsRequest request, CancellationToken ct)
    {
        var feature = await _rentalDocumentService.UpdateAsync(id, request, ct);

        return Ok(feature);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        await _rentalDocumentService.DeleteAsync(id, ct);

        return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery] string ids, CancellationToken ct)
    {
        var idsList = ids.Split(',').Select(Guid.Parse).ToArray();
        await _rentalDocumentService.DeleteAsync(idsList, ct);
        return NoContent();
    }
}
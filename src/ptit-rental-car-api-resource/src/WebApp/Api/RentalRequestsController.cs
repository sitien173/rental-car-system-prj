using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NGOT.ApplicationCore.Dto.RentalRequest;
using NGOT.ApplicationCore.Interfaces;

namespace NGOT.API.Api;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/rental-requests")]
[Authorize]
public class RentalRequestsController : BaseController
{
    private readonly IRentalRequestService _rentalRequestService;

    public RentalRequestsController(IRentalRequestService rentalRequestService)
    {
        _rentalRequestService = rentalRequestService;
    }

    [HttpGet]
    [Produces(typeof(RentalRequestResponse[]))]
    public async Task<IActionResult> Get(CancellationToken ct)
    {
        var results = await _rentalRequestService.GetAllAsync(ct);
        return Ok(results);
    }

    [HttpPut]
    [HttpPatch]
    [Route("{id}")]
    [Produces(typeof(RentalRequestResponse))]
    public async Task<IActionResult> Update(string id, UpdateRentalRequest request, CancellationToken ct)
    {
        var result = await _rentalRequestService.UpdateAsync(id, request, ct);

        return Ok(result);
    }

    [HttpDelete("{id}")]
    [HttpDelete]
    public async Task<IActionResult> Delete([FromRoute] string id, [FromQuery] string? ids, CancellationToken ct)
    {
        var idsList = ids?.Split(',').ToArray();
        if (idsList is { Length: > 0 })
        {
            await _rentalRequestService.DeleteAsync(idsList, ct);
            return NoContent();
        }

        await _rentalRequestService.DeleteAsync(id, ct);
        return NoContent();
    }
}
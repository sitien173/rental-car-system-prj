using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NGOT.ApplicationCore.Dto.RentalContract;
using NGOT.ApplicationCore.Interfaces;

namespace NGOT.API.Api;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/rental-contracts")]
[Authorize]
public class RentalContractsController : BaseController
{
    private readonly IRentalContractService _rentalContractService;

    public RentalContractsController(IRentalContractService rentalContractService)
    {
        _rentalContractService = rentalContractService;
    }

    [HttpGet]
    [Produces(typeof(RentalContractResponse[]))]
    public async Task<IActionResult> Get(CancellationToken ct)
    {
        var results = await _rentalContractService.GetAllAsync(ct);
        return Ok(results);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateRentalContractRequest request, CancellationToken ct)
    {
        var id = await _rentalContractService.CreateAsync(request, ct);
        return Ok(id);
    }

    [HttpPut]
    [HttpPatch]
    [Route("{id}")]
    [Produces(typeof(RentalContractResponse))]
    public async Task<IActionResult> Update(string id, UpdateRentalContractRequest request, CancellationToken ct)
    {
        var result = await _rentalContractService.UpdateAsync(id, request, ct);

        return Ok(result);
    }

    [HttpDelete("{id}")]
    [HttpDelete]
    public async Task<IActionResult> Delete([FromRoute] string id, [FromQuery] string? ids, CancellationToken ct)
    {
        var idsList = ids?.Split(',').ToArray();
        if (idsList is { Length: > 0 })
        {
            await _rentalContractService.DeleteAsync(idsList, ct);
            return NoContent();
        }

        await _rentalContractService.DeleteAsync(id, ct);
        return NoContent();
    }
}
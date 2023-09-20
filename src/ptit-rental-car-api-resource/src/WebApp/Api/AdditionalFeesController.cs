using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NGOT.ApplicationCore.Dto.AdditionalFee;
using NGOT.ApplicationCore.Dto.CarType;
using NGOT.ApplicationCore.Interfaces;

namespace NGOT.API.Api;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/additional-fees")]
[Authorize]
public class AdditionalFeesController : BaseController
{
    private readonly IAdditionalFeesService _additionalFeesService;

    public AdditionalFeesController(IAdditionalFeesService additionalFeesService)
    {
        _additionalFeesService = additionalFeesService;
    }

    [HttpGet]
    [Produces(typeof(AdditionalFeesResponse[]))]
    public async Task<IActionResult> Get(CancellationToken ct)
    {
        var additionalFees = await _additionalFeesService.GetAllAsync(ct);
        return Ok(additionalFees);
    }

    [HttpGet("{id:guid}")]
    [Produces(typeof(AdditionalFeesResponse))]
    public async Task<IActionResult> Get(Guid id, CancellationToken ct)
    {
        var additionalFees = await _additionalFeesService.GetByIdAsync(id, ct);
        return Ok(additionalFees);
    }

    [HttpPost]
    [Produces(typeof(CarTypeResponse))]
    public async Task<IActionResult> Create(CreateAdditionalFeesRequest request, CancellationToken ct)
    {
        var id = await _additionalFeesService.CreateAsync(request, ct);

        return Ok(id);
    }

    [HttpPut]
    [HttpPatch]
    [Route("{id:guid}")]
    [Produces(typeof(AdditionalFeesResponse))]
    public async Task<IActionResult> Update(Guid id, UpdateAdditionalFeesRequest request, CancellationToken ct)
    {
        var additionalFees = await _additionalFeesService.UpdateAsync(id, request, ct);

        return Ok(additionalFees);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        await _additionalFeesService.DeleteAsync(id, ct);

        return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery] string ids, CancellationToken ct)
    {
        var idsList = ids.Split(',').Select(Guid.Parse).ToArray();
        await _additionalFeesService.DeleteAsync(idsList, ct);
        return NoContent();
    }
}
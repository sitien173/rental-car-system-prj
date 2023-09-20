using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NGOT.ApplicationCore.Dto.VehicleHandover;
using NGOT.ApplicationCore.Interfaces;

namespace NGOT.API.Api;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/vehicle-handovers")]
[Authorize]
public class VehicleHandoversController : BaseController
{
    private readonly IVehicleHandoverService _vehicleHandoverService;

    public VehicleHandoversController(IVehicleHandoverService vehicleHandoverService)
    {
        _vehicleHandoverService = vehicleHandoverService;
    }

    [HttpGet]
    [Produces(typeof(VehicleHandoverResponse[]))]
    public async Task<IActionResult> Get(CancellationToken ct)
    {
        var features = await _vehicleHandoverService.GetAllAsync(ct);
        return Ok(features);
    }

    [HttpGet("{id:guid}")]
    [Produces(typeof(VehicleHandoverResponse))]
    public async Task<IActionResult> Get(Guid id, CancellationToken ct)
    {
        var feature = await _vehicleHandoverService.GetByIdAsync(id, ct);
        return Ok(feature);
    }

    [HttpPost]
    [Produces(typeof(VehicleHandoverResponse))]
    public async Task<IActionResult> Create(CreateVehicleHandoverRequest request, CancellationToken ct)
    {
        var id = await _vehicleHandoverService.CreateAsync(request, ct);

        return Ok(id);
    }

    [HttpPut]
    [HttpPatch]
    [Route("{id:guid}")]
    [Produces(typeof(VehicleHandoverResponse))]
    public async Task<IActionResult> Update(Guid id, UpdateVehicleHandoverRequest request, CancellationToken ct)
    {
        var feature = await _vehicleHandoverService.UpdateAsync(id, request, ct);

        return Ok(feature);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        await _vehicleHandoverService.DeleteAsync(id, ct);

        return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery] string ids, CancellationToken ct)
    {
        var idsList = ids.Split(',').Select(Guid.Parse).ToArray();
        await _vehicleHandoverService.DeleteAsync(idsList, ct);
        return NoContent();
    }
}
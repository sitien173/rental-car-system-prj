using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NGOT.ApplicationCore.Dto.Car;
using NGOT.ApplicationCore.Interfaces;

namespace NGOT.API.Api;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/cars")]
[Authorize]
public class CarsController : BaseController
{
    private readonly ICarService _carService;

    public CarsController(ICarService carService)
    {
        _carService = carService;
    }

    [HttpGet]
    [AllowAnonymous]
    [Produces(typeof(CarResponse[]))]
    public async Task<IActionResult> Get(CancellationToken ct = default)
    {
        var cars = await _carService.GetAllAsync(ct);
        return Ok(cars);
    }

    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    [Produces(typeof(CarResponse))]
    public async Task<IActionResult> Get(Guid id, CancellationToken ct)
    {
        var car = await _carService.GetByIdAsync(id, ct);
        return Ok(car);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateCarRequest request, CancellationToken ct)
    {
        var id = await _carService.CreateAsync(request, ct);

        return Ok(id);
    }

    [HttpPut]
    [HttpPatch]
    [Route("{id:guid}")]
    [Produces(typeof(CarResponse))]
    public async Task<IActionResult> Update(Guid id, UpdateCarRequest request, CancellationToken ct)
    {
        var car = await _carService.UpdateAsync(id, request, ct);

        return Ok(car);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        await _carService.DeleteAsync(id, ct);

        return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery] string ids, CancellationToken ct)
    {
        var idsList = ids.Split(',').Select(Guid.Parse).ToArray();
        await _carService.DeleteAsync(idsList, ct);
        return NoContent();
    }

    [Authorize]
    [HttpPost("{id:guid}/rent")]
    public async Task<IActionResult> Rent(Guid id, RentCarRequest request, CancellationToken ct)
    {
        var result = await _carService.RentAsync(id, request, ct);
        return result ? NoContent() : BadRequest();
    }
}
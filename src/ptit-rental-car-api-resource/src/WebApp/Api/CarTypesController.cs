using Microsoft.AspNetCore.Mvc;
using NGOT.ApplicationCore.Dto.CarType;
using NGOT.ApplicationCore.Interfaces;

namespace NGOT.API.Api;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/car-types")]
public class CarTypesController : BaseController
{
    private readonly ICarTypeService _carTypeService;

    public CarTypesController(ICarTypeService carTypeService)
    {
        _carTypeService = carTypeService;
    }

    [HttpGet]
    [Produces(typeof(CarTypeResponse))]
    public async Task<IActionResult> Get(CancellationToken ct)
    {
        var carTypes = await _carTypeService.GetAllAsync(ct);
        return Ok(carTypes);
    }

    [HttpGet("{id:guid}")]
    [Produces(typeof(CarTypeResponse))]
    public async Task<IActionResult> Get(Guid id, CancellationToken ct)
    {
        var carType = await _carTypeService.GetByIdAsync(id, ct);
        return Ok(carType);
    }

    [HttpPost]
    [Produces(typeof(CarTypeResponse))]
    public async Task<IActionResult> Create(CreateCarTypeRequest request, CancellationToken ct)
    {
        var id = await _carTypeService.CreateAsync(request, ct);

        return Ok(id);
    }

    [HttpPut]
    [HttpPatch]
    [Route("{id:guid}")]
    [Produces(typeof(CarTypeResponse))]
    public async Task<IActionResult> Update(Guid id, UpdateCarTypeRequest request, CancellationToken ct)
    {
        var carType = await _carTypeService.UpdateAsync(id, request, ct);

        return Ok(carType);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        await _carTypeService.DeleteAsync(id, ct);

        return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery] string ids, CancellationToken ct)
    {
        var idsList = ids.Split(',').Select(Guid.Parse).ToArray();
        await _carTypeService.DeleteAsync(idsList, ct);
        return NoContent();
    }
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NGOT.ApplicationCore.Dto.Brand;
using NGOT.ApplicationCore.Interfaces;

namespace NGOT.API.Api;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/brands")]
[Authorize]
public class BrandsController : BaseController
{
    private readonly IBrandService _brandService;
    public BrandsController(IBrandService brandService)
    {
        _brandService = brandService;
    }

    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken ct = default)
    {
        var results = await _brandService.GetAllAsync(ct);
        return Ok(results);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateBrandRequest request, CancellationToken ct)
    {
        var id = await _brandService.CreateAsync(request, ct);
        return Ok(id);
    }

    [HttpPut]
    [HttpPatch]
    [Route("{id:guid}")]
    [Produces(typeof(BrandResponse))]
    public async Task<IActionResult> Update(Guid id, UpdateBrandRequest request, CancellationToken ct)
    {
        var brand = await _brandService.UpdateAsync(id, request, ct);

        return Ok(brand);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        await _brandService.DeleteAsync(id, ct);

        return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery] string ids, CancellationToken ct)
    {
        var idsList = ids.Split(',').Select(Guid.Parse).ToArray();
        await _brandService.DeleteAsync(idsList, ct);
        return NoContent();
    }
}
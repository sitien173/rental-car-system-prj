using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NGOT.ApplicationCore.Dto.CarType;
using NGOT.ApplicationCore.Dto.Feature;
using NGOT.ApplicationCore.Interfaces;

namespace NGOT.API.Api;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/features")]
[Authorize]
public class FeaturesController : BaseController
{
    private readonly IFeatureService _featureService;

    public FeaturesController(IFeatureService featureService)
    {
        _featureService = featureService;
    }

    [HttpGet]
    [Produces(typeof(FeatureResponse[]))]
    public async Task<IActionResult> Get(CancellationToken ct)
    {
        var features = await _featureService.GetAllAsync(ct);
        return Ok(features);
    }

    [HttpGet("{id:guid}")]
    [Produces(typeof(FeatureResponse))]
    public async Task<IActionResult> Get(Guid id, CancellationToken ct)
    {
        var feature = await _featureService.GetByIdAsync(id, ct);
        return Ok(feature);
    }

    [HttpPost]
    [Produces(typeof(CarTypeResponse))]
    public async Task<IActionResult> Create(CreateFeatureRequest request, CancellationToken ct)
    {
        var id = await _featureService.CreateAsync(request, ct);

        return Ok(id);
    }

    [HttpPut]
    [HttpPatch]
    [Route("{id:guid}")]
    [Produces(typeof(FeatureResponse))]
    public async Task<IActionResult> Update(Guid id, UpdateFeatureRequest request, CancellationToken ct)
    {
        var feature = await _featureService.UpdateAsync(id, request, ct);

        return Ok(feature);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        await _featureService.DeleteAsync(id, ct);

        return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery] string ids, CancellationToken ct)
    {
        var idsList = ids.Split(',').Select(Guid.Parse).ToArray();
        await _featureService.DeleteAsync(idsList, ct);
        return NoContent();
    }
}
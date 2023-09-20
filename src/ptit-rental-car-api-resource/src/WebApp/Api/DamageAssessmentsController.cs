using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NGOT.ApplicationCore.Dto.DamageAssessment;
using NGOT.ApplicationCore.Interfaces;

namespace NGOT.API.Api;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/damage-assessments")]
[Authorize]
public class DamageAssessmentsController : BaseController
{
    private readonly IDamageAssessmentService _damageAssessmentService;

    public DamageAssessmentsController(IDamageAssessmentService damageAssessmentService)
    {
        _damageAssessmentService = damageAssessmentService;
    }

    [HttpGet]
    [Produces(typeof(DamageAssessmentResponse[]))]
    public async Task<IActionResult> Get(CancellationToken ct)
    {
        var features = await _damageAssessmentService.GetAllAsync(ct);
        return Ok(features);
    }

    [HttpGet("{id:guid}")]
    [Produces(typeof(DamageAssessmentResponse))]
    public async Task<IActionResult> Get(Guid id, CancellationToken ct)
    {
        var feature = await _damageAssessmentService.GetByIdAsync(id, ct);
        return Ok(feature);
    }

    [HttpPost]
    [Produces(typeof(DamageAssessmentResponse))]
    public async Task<IActionResult> Create(CreateDamageAssessmentRequest request, CancellationToken ct)
    {
        var id = await _damageAssessmentService.CreateAsync(request, ct);

        return Ok(id);
    }

    [HttpPut]
    [HttpPatch]
    [Route("{id:guid}")]
    [Produces(typeof(DamageAssessmentResponse))]
    public async Task<IActionResult> Update(Guid id, UpdateDamageAssessmentRequest request, CancellationToken ct)
    {
        var feature = await _damageAssessmentService.UpdateAsync(id, request, ct);

        return Ok(feature);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        await _damageAssessmentService.DeleteAsync(id, ct);

        return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery] string ids, CancellationToken ct)
    {
        var idsList = ids.Split(',').Select(Guid.Parse).ToArray();
        await _damageAssessmentService.DeleteAsync(idsList, ct);
        return NoContent();
    }
}
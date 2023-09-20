using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NGOT.ApplicationCore.Dto.Payment;
using NGOT.ApplicationCore.Interfaces;

namespace NGOT.API.Api;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/payments")]
[Authorize]
public class PaymentsController : BaseController
{
    private readonly IPaymentService _paymentService;

    public PaymentsController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    [HttpGet]
    [Produces(typeof(PaymentResponse[]))]
    public async Task<IActionResult> Get(CancellationToken ct)
    {
        var features = await _paymentService.GetAllAsync(ct);
        return Ok(features);
    }

    [HttpGet("{id:guid}")]
    [Produces(typeof(PaymentResponse))]
    public async Task<IActionResult> Get(Guid id, CancellationToken ct)
    {
        var feature = await _paymentService.GetByIdAsync(id, ct);
        return Ok(feature);
    }

    [HttpPost]
    [Produces(typeof(PaymentResponse))]
    public async Task<IActionResult> Create(CreatePaymentRequest request, CancellationToken ct)
    {
        var id = await _paymentService.CreateAsync(request, ct);

        return Ok(id);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        await _paymentService.DeleteAsync(id, ct);

        return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery] string ids, CancellationToken ct)
    {
        var idsList = ids.Split(',').Select(Guid.Parse).ToArray();
        await _paymentService.DeleteAsync(idsList, ct);
        return NoContent();
    }
}
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NGOT.Common.Interfaces;

namespace NGOT.API.Api;

public class BaseController : ControllerBase
{
    private readonly JsonSerializerSettings _jsonSerializerSettings = new()
    {
        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
        NullValueHandling = NullValueHandling.Ignore,
        Formatting = Formatting.Indented,
        ContractResolver = new CamelCasePropertyNamesContractResolver()
    };

    private IMapper? _mapper;

    protected IMapper Mapper => _mapper ??= HttpContext.RequestServices.GetRequiredService<IMapper>();

    protected string UserId =>
        User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value ?? string.Empty;

    public override OkObjectResult Ok(object? value)
    {
        return base.Ok(JsonConvert.SerializeObject(value, _jsonSerializerSettings));
    }

    public override BadRequestObjectResult BadRequest(object? error)
    {
        var problemDetails = new ProblemDetails
        {
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            Title = "Bad Request",
            Status = StatusCodes.Status400BadRequest,
            Detail = "Please refer to the errors property for additional details.",
            Instance = Request.Path
        };

        problemDetails.Extensions.Add("timestamp",
            HttpContext.RequestServices.GetRequiredService<IDateTimeProvider>().Now);
        problemDetails.Extensions.Add("traceId", HttpContext.TraceIdentifier);
        problemDetails.Extensions.Add("errors", error);

        return base.BadRequest(JsonConvert.SerializeObject(problemDetails, _jsonSerializerSettings));
    }
}
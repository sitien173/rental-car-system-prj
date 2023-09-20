using FluentValidation;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Serilog.Core;

namespace NGOT.API.ErrorHandlers;

public class GlobalHandleErrorMiddleware
{
    private readonly RequestDelegate _next;

    public GlobalHandleErrorMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception e)
        {
            Logger.None.Error(e, "An unexpected error occurred! {ErrorMessage}", e.Message);
            await HandleError(context, e);
        }
    }

    private static async Task HandleError(HttpContext context, Exception exception)
    {
        var problemDetails = CreateProblemDetails(context, exception);

        context.Response.StatusCode = problemDetails.Status!.Value;
        context.Response.ContentType = "application/json";

        await context.Response.WriteAsync(JsonConvert.SerializeObject(problemDetails, new JsonSerializerSettings
        {
            DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = NullValueHandling.Ignore,
            Formatting = Formatting.Indented,
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        }));
    }

    private static Error CreateProblemDetails(HttpContext context, Exception exception)
    {
        var problemDetails = new Error()
        {
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            Title = "An unexpected error occurred!",
            Status = StatusCodes.Status500InternalServerError,
            Detail = exception.Message,
            Instance = context.Request.Path,
            TraceId = context.TraceIdentifier
        };
        
        if (exception is ValidationException validationException)
        {
            problemDetails.Title = "Validation error occurred!";
            problemDetails.Status = StatusCodes.Status400BadRequest;
            problemDetails.Detail = "One or more validation errors occurred!";
            problemDetails.Errors = validationException
                .Errors
                .GroupBy(x => x.PropertyName)
                .ToDictionary(x => x.Key, x => 
                    x.Select(y => y.ErrorMessage));
        }
        else if (exception is ArgumentException argumentException)
        {
            problemDetails.Title = "One or more errors occurred!";
            problemDetails.Status = StatusCodes.Status400BadRequest;
            problemDetails.Detail = argumentException.Message;
            problemDetails.Errors = new Dictionary<string, IEnumerable<string>>(1)
            {
                { argumentException.ParamName ?? "error", new [] { argumentException.Message } }
            };
        }
        else if (exception is TimeoutException)
        {
            problemDetails.Title = "Request timeout!";
            problemDetails.Status = StatusCodes.Status408RequestTimeout;
            problemDetails.Detail = "The request timed out!";
        }
        else
        {
            var message = exception.InnerException != null ? exception.InnerException.Message : exception.Message;
            problemDetails.Detail = message;
            problemDetails.Errors = new Dictionary<string, IEnumerable<string>>(1)
            {
                { "error", new [] { message } }
            };
        }

        return problemDetails;
    }
}
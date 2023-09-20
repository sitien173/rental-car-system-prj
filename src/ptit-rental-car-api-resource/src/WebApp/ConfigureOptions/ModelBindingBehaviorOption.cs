using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace NGOT.API.ConfigureOptions;

public class ModelBindingBehaviorOption : IConfigureOptions<ApiBehaviorOptions>
{
    public void Configure(ApiBehaviorOptions options)
    {
        options.InvalidModelStateResponseFactory = context =>
        {
            var errors = new List<ValidationFailure>();
            foreach (var modelState in context.ModelState)
            foreach (var error in modelState.Value.Errors)
            {
                var validationFailure = new ValidationFailure
                {
                    PropertyName = modelState.Key,
                    ErrorMessage = error.ErrorMessage,
                    AttemptedValue = modelState.Value.AttemptedValue,
                    Severity = Severity.Error
                };
                errors.Add(validationFailure);
            }

            throw new ValidationException(errors);
        };
    }
}
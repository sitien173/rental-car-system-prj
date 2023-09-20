using FluentValidation;

namespace NGOT.ApplicationCore.Dto.Feature.Validations;

public class CreateFeatureRequestValidator : AbstractValidator<CreateFeatureRequest>
{
    public CreateFeatureRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Icon)
            .NotEmpty();

        RuleFor(x => x.Icon.Host)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.Icon.FileName)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.Icon.Size)
            .GreaterThan(0);

        RuleFor(x => x.Icon.Type)
            .NotEmpty()
            .MaximumLength(5);
    }
}
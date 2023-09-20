using FluentValidation;

namespace NGOT.ApplicationCore.Dto.AdditionalFee.Validations;

public class CreateAdditionalFeesRequestValidator : AbstractValidator<CreateAdditionalFeesRequest>
{
    public CreateAdditionalFeesRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(50);
        RuleFor(x => x.Description)
            .MaximumLength(500);
        RuleFor(x => x.Price)
            .NotEmpty()
            .GreaterThan(0);
        RuleFor(x => x.Unit)
            .MaximumLength(50);
    }
}
using FluentValidation;

namespace NGOT.ApplicationCore.Dto.Car.Validations;

public class CreateCarRequestValidator : AbstractValidator<CreateCarRequest>
{
    public CreateCarRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Specificity).NotEmpty();
        RuleFor(x => x.Description).NotEmpty().MaximumLength(500);
        RuleFor(x => x.BrandId).NotEmpty();
        RuleFor(x => x.CarTypeId).NotEmpty();
        RuleFor(x => x.Price).NotEmpty().GreaterThan(0);
        RuleFor(x => x.Rule).NotEmpty().MaximumLength(500);
    }
}
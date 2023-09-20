using FluentValidation;

namespace NGOT.ApplicationCore.Dto.CarType.Validations;

public class CreateCarTypeRequestValidator : AbstractValidator<CreateCarTypeRequest>
{
    public CreateCarTypeRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(50);

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
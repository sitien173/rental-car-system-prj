using FluentValidation;

namespace NGOT.ApplicationCore.Dto.Brand.Validations;

public class UpdateBrandRequestValidator : AbstractValidator<UpdateBrandRequest>
{
    public UpdateBrandRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty();
    }
}
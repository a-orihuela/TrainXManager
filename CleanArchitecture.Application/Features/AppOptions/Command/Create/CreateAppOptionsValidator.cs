using FluentValidation;

namespace CleanArchitecture.Application.Features.AppOptions.Command.Create
{
    public class CreateAppOptionsValidator : AbstractValidator<CreateAppOptionsCommand>
    {
        public CreateAppOptionsValidator()
        {
            RuleFor(p => p.RolName)
                .NotEmpty().WithMessage("{RolName} no puede estar en blanco")
                .NotNull();
        }
    }
}

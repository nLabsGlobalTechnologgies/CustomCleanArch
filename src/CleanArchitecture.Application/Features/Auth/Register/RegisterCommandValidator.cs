using FluentValidation;

namespace CleanArchitecture.Application.Features.Auth.Register;
internal class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(u => u.FirstName).NotEmpty();
        RuleFor(u => u.FirstName).NotNull();
        RuleFor(u => u.FirstName).MinimumLength(3);
    }
}

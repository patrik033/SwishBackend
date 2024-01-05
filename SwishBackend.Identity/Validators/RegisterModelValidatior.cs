using FluentValidation;
using SwishBackend.Models.Dto;

namespace SwishBackend.Identity.Validators
{
    public class RegisterModelValidatior : AbstractValidator<RegisterRequestDTO>
    {
        public RegisterModelValidatior()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}

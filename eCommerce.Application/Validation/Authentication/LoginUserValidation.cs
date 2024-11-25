using eCommerce.Application.DTOs.Identity;
using FluentValidation;

namespace eCommerce.Application.Validation.Authentication
{
    public class LoginUserValidation : AbstractValidator<LoginUser>
    {
        public LoginUserValidation()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is requird")
               .EmailAddress().WithMessage("Invalid email format");

            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is requird");


        }
    }
}

using eCommerce.Application.DTOs.Identity;
using FluentValidation;

namespace eCommerce.Application.Validation.Authentication
{
    public class CreateUserValidation : AbstractValidator<CreateUser>
    {
        public CreateUserValidation()
        {
            RuleFor(x => x.FullName).NotEmpty().WithMessage("FullName is requird");

            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is requird")
                .EmailAddress().WithMessage("Invalid email format");

            RuleFor(x=>x.Password).NotEmpty().WithMessage("Password is requird")
                .MinimumLength(8).WithMessage("Password must be at least 8 chars")
                .Matches(@"[a-z]").WithMessage("Password must have at least 1 small char ")
                .Matches(@"[A-Z]").WithMessage("Password must have at least 1 capital char ");
            RuleFor(x => x.ConfirmPassword).Equal(x => x.Password).WithMessage("Password do not match");
            
        }
    }
}

using FluentValidation;

namespace CouponManagementService.WebApi.Models
{
    public class User
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

    }

    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().NotNull().WithMessage("Username cannot be empty");
            RuleFor(x => x.UserName).MinimumLength(5).WithMessage("Username cannot be shorter than 5 characters");

            RuleFor(x => x.Password).NotEmpty().NotNull().WithMessage("Password cannot be empty");
            RuleFor(x => x.Password).MinimumLength(8).WithMessage("Password cannot be shorter than 8 characters");

            RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage("Name cannot be empty");

            RuleFor(x => x.Email).NotEmpty().NotNull().WithMessage("Email cannot be empty");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Email format is wrong");

            RuleFor(x => x.PhoneNumber).Length(11).WithMessage("Phone format is wrong");
        }
    }

    public class Login
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

 

}

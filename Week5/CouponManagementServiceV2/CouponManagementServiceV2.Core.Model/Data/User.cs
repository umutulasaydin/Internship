﻿using CouponManagementServiceV2.Core.Model.Shared;
using FluentValidation;

namespace CouponManagementServiceV2.Core.Model.Data
{
    public class Users : BaseRequest
    {
        public int usId { get; set; } = -1;
        public string usUsername { get; set; }
        public string usPassword { get; set; }
        public string usName { get; set; }
        public string usMail { get; set; }
        public string usPhoneNum { get; set; } = "00000000000";
    }

    public class UserValidator : AbstractValidator<Users>
    {
        public UserValidator()
        {
          

            RuleFor(x => x.usUsername).NotEmpty().NotNull().WithMessage("Username cannot be empty");
            RuleFor(x => x.usUsername).MinimumLength(5).WithMessage("Username cannot be shorter than 5 characters");

            RuleFor(x => x.usPassword).NotEmpty().NotNull().WithMessage("Password cannot be empty");
            RuleFor(x => x.usPassword).MinimumLength(8).WithMessage("Password cannot be shorter than 8 characters");

            RuleFor(x => x.usName).NotEmpty().NotNull().WithMessage("Name cannot be empty");

            RuleFor(x => x.usMail).NotEmpty().NotNull().WithMessage("Email cannot be empty");
            RuleFor(x => x.usMail).EmailAddress().WithMessage("Email format is wrong");

            RuleFor(x => x.usPhoneNum).Length(11).WithMessage("Phone format is wrong");
        }
    }

    public class Login : BaseRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

}

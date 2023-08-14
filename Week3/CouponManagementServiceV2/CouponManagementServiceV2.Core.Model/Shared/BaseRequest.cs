using FluentValidation;

namespace CouponManagementServiceV2.Core.Model.Shared
{
    public class BaseRequest
    {
        public string ClientName { get; set; }
        public string ClientPos { get; set; }
    }

    public class BaseRequestValidator : AbstractValidator<BaseRequest>
    {
        public BaseRequestValidator()
        {
            RuleFor(x => x.ClientName).NotNull().NotEmpty().WithMessage("Client name cannot be empty");

            RuleFor(x => x.ClientPos).NotNull().NotEmpty().WithMessage("Client pos cannot be empty");
        }
    }
}

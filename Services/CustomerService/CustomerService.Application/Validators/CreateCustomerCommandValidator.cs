using CustomerService.Application.Commands;
using FluentValidation;

namespace CustomerService.Application.Validators
{
    public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerCommandValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("نام الزامی است.")
                .MinimumLength(3).WithMessage("نام باید حداقل ۳ حرف باشد.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("ایمیل الزامی است.")
                .EmailAddress().WithMessage("فرمت ایمیل نادرست است.");
        }
    }
}

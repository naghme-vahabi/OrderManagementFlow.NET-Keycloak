using FluentValidation;
using OrderService.Application.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Validators
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(v => v.CustomerId)
                .NotEmpty().WithMessage("کد مشتری الزامی است.");

            RuleFor(v => v.Items)
                .NotEmpty().WithMessage("سفارش باید حداقل شامل یک کالا باشد.");

            RuleForEach(v => v.Items).ChildRules(item =>
            {
                item.RuleFor(x => x.ProductId)
                    .NotEmpty().WithMessage("کد محصول الزامی است.");

                item.RuleFor(x => x.Quantity)
                    .GreaterThan(0).WithMessage("تعداد محصول باید بزرگتر از صفر باشد.");
            });
        }
    }
}

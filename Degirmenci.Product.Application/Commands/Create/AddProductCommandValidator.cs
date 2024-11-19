using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Degirmenci.Product.Application.Commands.Create
{
    public class AddProductCommandValidator:AbstractValidator<AddProductCommand>
    {
        public AddProductCommandValidator()
        {
            RuleFor(p=>p.Name).NotEmpty().MinimumLength(2);
            RuleFor(P => P.Price).NotEmpty().GreaterThan(0);
            RuleFor(p => p.Description).NotEmpty();
            RuleFor(p => p.Stock).NotEmpty();
        }
    }
}

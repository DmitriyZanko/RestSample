using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using RestSample.Data.Contexts;
using RestSample.Logic.Models;

namespace RestSample.Logic.Validators
{
    public class PizzaDtoValidator : AbstractValidator<PizzaDto>
    {
        private readonly PizzaShopContext _context;

        public PizzaDtoValidator(PizzaShopContext context)
        {
            _context = context;
            RuleSet("PreValidation", () =>
            {
                RuleFor(p => p.Name).NotNull().MinimumLength(5)
                    .WithMessage("Field Name is invalid");
            });

            RuleSet("PostValidation", () =>
            {
                RuleFor(p => p.Name).Must(CheckDuplicate);

            });
        }

        private bool CheckDuplicate(string name)
        {
            return !_context.Pizzas.AsNoTracking().Any(p => p.Name == name);
        }
    }
}

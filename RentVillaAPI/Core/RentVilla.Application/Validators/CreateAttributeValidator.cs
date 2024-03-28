using FluentValidation;
using RentVilla.Application.ViewModels.Attribute;
using RentVilla.Domain.Entities.Concrete.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Application.Validators
{
    public class CreateAttributeValidator:AbstractValidator<AttributeCreateVM>
    {
        public CreateAttributeValidator()
        {
            RuleFor(a => a.Description)
                .NotEmpty()
                .NotNull().WithMessage("Attribute description cannot be empty.")
                .MaximumLength(150)
                .MinimumLength(5)
                .WithMessage("Attribute description must be between 5 and 150 characters.");
        }
    }
}

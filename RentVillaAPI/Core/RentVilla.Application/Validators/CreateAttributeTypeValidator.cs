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
    public class CreateAttributeTypeValidator:AbstractValidator<AttributeType>
    {
        public CreateAttributeTypeValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Name).MaximumLength(50).WithMessage("Name can not be longer than 50 characters");
            RuleFor(x => x.Name).MinimumLength(3).WithMessage("Name can not be shorter than 3 characters");
        }
    }
}

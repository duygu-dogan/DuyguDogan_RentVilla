using FluentValidation;
using RentVilla.Application.ViewModels.Product;

namespace RentVilla.Application.Validators
{
    public class CreateProductValidator: AbstractValidator<ProductCreateVM>
    {
        public CreateProductValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .NotNull().WithMessage("Product name cannot be empty.")
                .MaximumLength(150)
                .MinimumLength(5)
                .WithMessage("Product name must be between 5 and 150 characters.");
            RuleFor(p => p.Address)
                .NotEmpty()
                .NotNull().WithMessage("Product address cannot be empty.")
                .MaximumLength(150)
                .MinimumLength(5)
                .WithMessage("Product address must be between 5 and 150 characters.");
            RuleFor(p => p.ShortestRentPeriod)
                .NotEmpty()
                .NotNull().WithMessage("Shortest rent period cannot be empty.")
                .GreaterThan(0)
                .WithMessage("Shortest rent period must be greater than 0.");
            RuleFor(p => p.MapId)
                .NotEmpty()
                .NotNull().WithMessage("Map id cannot be empty.");
            RuleFor(p =>p.Price)
                .NotEmpty()
                .NotNull().WithMessage("Price cannot be empty.")
                .GreaterThan(0)
                .WithMessage("Price must be greater than 0.");
            RuleFor(p=> p.Deposit)
                .NotEmpty()
                .NotNull().WithMessage("Deposit cannot be empty.")
                .GreaterThan(0)
                .WithMessage("Deposit must be greater than 0.");
            RuleFor(p => p.ProductAddress)
                .NotNull().WithMessage("Product address cannot be empty.");
            RuleFor(p => p.AttributeIDs)
                .NotNull().WithMessage("Product attributes cannot be empty.");
        }
    }
}

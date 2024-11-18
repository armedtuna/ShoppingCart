using FluentValidation;

namespace ShoppingCart.Entities.Validators;

// todo-at: validation? for example if user is allowed to manually enter the quantity. also where would stock levels be checked?
// todo-at: tests? tests for incoming null?
public class ScanProductValidator : AbstractValidator<ScanProduct>
{
    public ScanProductValidator()
    {
        RuleFor(product => product.Sku).NotEmpty().WithName("SKU");
    }
}

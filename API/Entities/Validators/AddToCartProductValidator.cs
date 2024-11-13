using FluentValidation;

namespace ShoppingCart.Entities.Validators;

public class AddToCartProductValidator : AbstractValidator<Product>
{
    public AddToCartProductValidator()
    {
        RuleFor(product => product.Sku).NotEmpty();
        RuleFor(product => product.UnitPrice).Empty();
        RuleFor(product => product.SpecialPriceName).Empty();
        RuleFor(product => product.SpecialPrice).Empty();
    }
}
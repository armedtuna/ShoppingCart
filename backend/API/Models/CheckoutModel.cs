using FluentValidation.Results;
using ShoppingCart.Entities;
using ShoppingCart.Entities.Validators;

namespace ShoppingCart.Models;

public class CheckoutModel()
{
    private static readonly Checkout Checkout = Data.ExampleData.BuildCheckout();
    // todo-at: how are multiple users supported? out-of-scope for this project? probably
    public static readonly CheckoutModel Instance = new CheckoutModel();

    public Checkout GetCart() =>
        Checkout;

    public IResult Scan(ScanProduct scanProduct)
    {
        ScanProductValidator validator = new();
        ValidationResult validationResult = validator.Validate(scanProduct);
        if (!validationResult.IsValid)
        {
            IEnumerable<string> errors = validationResult.Errors
                .Select(x => x.ErrorMessage);
            return Results.BadRequest(string.Join(' ', errors));
        }
        
        Product? product = ProductModel.Instance.RetrieveProduct(scanProduct.Sku);
        if (product == null)
        {
            return Results.NotFound($"SKU '{scanProduct.Sku}' not found.");
        }

        Checkout.Scan(product);
        return Results.Ok();
    }
}

using FluentValidation.Results;
using ShoppingCart.Entities;
using ShoppingCart.Entities.Validators;

namespace ShoppingCart.Models;

public class CheckoutModel()
{
    private static readonly Checkout Checkout = Data.ExampleData.BuildCheckout();
    private static readonly ScanProductValidator ScanProductValidator = new();
    // todo-at: how are multiple users supported? out-of-scope for this project? probably
    public static readonly CheckoutModel Instance = new CheckoutModel();

    public Checkout GetCart() =>
        Checkout;

    public IResult Scan(ScanProduct scanProduct)
    {
        ValidationResult validationResult = ScanProductValidator.Validate(scanProduct);
        if (!validationResult.IsValid) return InvalidScanProduct(validationResult);

        Product? product = ProductModel.Instance.RetrieveProduct(scanProduct.Sku);
        if (product == null) return ProductNotFound(scanProduct);

        Checkout.Scan(product);
        return Results.Ok();
    }

    public IResult Delete(string sku)
    {
        if (!Checkout.Delete(sku))
        {
            return ProductNotFound(sku);
        }

        return Results.Ok();
    }

    private static IResult ProductNotFound(ScanProduct scanProduct) =>
        ProductNotFound(scanProduct.Sku);

    private static IResult ProductNotFound(string sku) =>
        Results.NotFound($"SKU '{sku}' not found.");

    private static IResult InvalidScanProduct(ValidationResult validationResult)
    {
        IEnumerable<string> errors = validationResult.Errors
            .Select(x => x.ErrorMessage);
        return Results.BadRequest(errors);
    }
}

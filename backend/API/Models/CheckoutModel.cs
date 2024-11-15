using FluentValidation.Results;
using ShoppingCart.Entities;
using ShoppingCart.Entities.Validators;

namespace ShoppingCart.Models;

public class CheckoutModel()
{
    private static readonly Checkout Checkout = Data.ExampleData.BuildCheckout();
    // todo-at: how are multiple users supported? out-of-scope for this project? probably
    public static readonly CheckoutModel Instance = new CheckoutModel();
    
    private Product[] _products = Data.ExampleData.BuildProducts();

    public Checkout GetCart() =>
        Checkout;

    // todo-at: what should the response be so that HTTP code are returned appropriately?
    public ScanResult Scan(Product product)
    {
        // HttpResponseMessage response = new HttpResponseMessage();
        // response.Content = 
        AddToCartProductValidator addToCartProductValidator = new();
        ValidationResult validationResult = addToCartProductValidator.Validate(product);
        if (!validationResult.IsValid)
        {
            return new ScanResult
            {
                Success = false,
                Errors = validationResult.Errors
                    .Select(error => error.ErrorMessage)
                    .ToArray(),
                TotalPrice = Checkout.TotalPrice
            };
        }
        
        Product matchingProduct = _products.First(p => p.Sku == product.Sku);
        Checkout.Scan(matchingProduct);
        return new ScanResult()
        {
            Success = true,
            TotalPrice = Checkout.TotalPrice
        };
    }
}

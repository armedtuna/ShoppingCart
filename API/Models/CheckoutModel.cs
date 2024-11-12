using ShoppingCart.Entities;

namespace ShoppingCart.Models;

public class CheckoutModel()
{
    private static readonly Checkout Checkout = Data.ExampleCheckout.Build();
    // todo-at: how are multiple users supported? out-of-scope for this project? probably
    public static readonly CheckoutModel Instance = new CheckoutModel();

    public Checkout GetCart() =>
        Checkout;

    public void AddProduct(Product product) =>
        Checkout.Scan(product);
}

using ShoppingCart.Entities;

namespace ShoppingCart.Models;

public class CartModel()
{
    private static readonly Cart Cart = Data.ExampleCart.Build();
    // todo-at: how are multiple users supported? out-of-scope for this project? probably
    public static readonly CartModel Instance = new CartModel();

    public Cart GetCart() =>
        Cart;

    public void AddProduct(Product product) =>
        Cart.AddProduct(product);
}

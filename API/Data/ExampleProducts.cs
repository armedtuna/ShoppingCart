using ShoppingCart.Entities;

namespace ShoppingCart.Data;

public class ExampleProducts
{
    public static Product[] Build()
    {
        return
        [
            new Product("A", 50),
            new Product("B", 30),
            new Product("C", 20),
            new Product("D", 15)
        ];
    }
}

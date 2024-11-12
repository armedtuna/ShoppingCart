using ShoppingCart.Entities;

namespace ShoppingCart.Models;

public class ProductModel
{
    public static readonly ProductModel Instance = new ProductModel();
    
    public Product[] RetrieveProducts()
    {
        return Data.ExampleProducts.Build();
    }
}

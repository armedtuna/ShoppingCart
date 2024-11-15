using ShoppingCart.Entities;

namespace ShoppingCart.Models;

public class ProductModel
{
    public static readonly ProductModel Instance = new ProductModel();
    
    public Product[] RetrieveProducts()
    {
        return Data.ExampleProducts.Build();
    }

    // todo-at: add product feature, and support save to JSON file?
    // public bool AddProduct(Product product)
    // {
    //     // todo-at: validation to ensure it's clean
    // }

    // todo-at: extract data to JSON file?
    // public bool LoadData(string path)
    // {
    //     Newtonsoft.JSON.deserial(path)
    // }
}

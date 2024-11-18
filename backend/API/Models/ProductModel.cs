using ShoppingCart.Entities;

namespace ShoppingCart.Models;

public class ProductModel
{
    private readonly Product[] _products = Data.ExampleData.BuildProducts();

    public static readonly ProductModel Instance = new ProductModel();
    
    public Product[] RetrieveProducts()
    {
        return _products;
    }

    public Product? RetrieveProduct(string sku) =>
        _products.FirstOrDefault(p => p.Sku == sku);

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

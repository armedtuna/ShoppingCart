using ShoppingCart.Entities;
using ShoppingCart.Entities.ShoppingRules;

namespace Tests.Entities;

public class DataHelper
{
    private readonly Product[] _products =
    [
        new("a", 50),
        new("b", 30),
        new("c", 20),
        new("d", 15)
    ];

    private readonly IShoppingRule[] _shoppingRules =
    [
        new SameOfMultipleProductShoppingRule("a", 3, 130),
        new SameOfMultipleProductShoppingRule("b", 2, 45),
        new SameOfMultipleProductShoppingRule("a", 5, 150),
    ];

    public Product[] BuildProducts() =>
        _products;

    public IShoppingRule[] GetShoppingRules() =>
        _shoppingRules;

    public Product GetProduct(char sku) =>
        _products.First(p => p.Sku == sku.ToString());
}

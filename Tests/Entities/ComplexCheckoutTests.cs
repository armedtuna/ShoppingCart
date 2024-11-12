using ShoppingCart.Data;
using ShoppingCart.Entities;
using ShoppingCart.Entities.ShoppingRules;

namespace Tests.Entities;

public class ComplexCheckoutTests
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
        new LoyaltyShoppingRule(["a", "b"], 3),
    ];

    [TestCase("a", 48.5)]
    // todo-at: this combination raises interesting questions re multiple rules:
    // - should they be cumulative?
    // - should they override each other for best offer?
    // - should only one rule apply?
    [TestCase("aaa", 126.1)]
    public void TestTotals(string skus, decimal expectedTotalPrice)
    {
        Checkout checkout = new(_shoppingRules);
        foreach (char sku in skus)
        {
            Product matchingProduct = GetProduct(sku);
            checkout.Scan(matchingProduct);
        }
        
        Assert.That(checkout.Products.Count, Is.EqualTo(skus.Length));
        Assert.That(checkout.TotalPrice, Is.EqualTo(expectedTotalPrice));
    }

    // todo-at: extract to helper? along with _products?
    private Product GetProduct(char sku) =>
        _products.First(p => p.Sku == sku.ToString());
}
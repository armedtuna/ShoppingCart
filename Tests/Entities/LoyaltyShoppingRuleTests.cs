using ShoppingCart.Entities;
using ShoppingCart.Entities.ShoppingRules;

namespace Tests.Entities;

[TestFixture]
public class LoyaltyShoppingRuleTests
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
        new LoyaltyShoppingRule(new string[] { "a", "b", "c" }, 3),
    ];
    
    // todo-at: try to mix up the same-of rule and the discount rule.
    // - there should be some interesting results, especially if the discount rule is applied first? :)
    [TestCase("abcd", 97 + 15)]
    [TestCase("a", 48.5F)]
    [TestCase("b", 29.1F)]
    public void TestTotals(string skus, float expectedTotalPrice)
    {
        Checkout checkout = new Checkout(_shoppingRules);
        foreach (char sku in skus)
        {
            Product matchingProduct = GetProduct(sku);
            checkout.Scan(matchingProduct);
        }
        
        Assert.That(checkout.Products.Count, Is.EqualTo(skus.Length));
        Assert.That(checkout.TotalPrice, Is.EqualTo(expectedTotalPrice));
    }
    
    private Product GetProduct(char sku) =>
        _products.First(p => p.Sku == sku.ToString());
}

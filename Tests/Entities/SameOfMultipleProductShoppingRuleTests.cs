using ShoppingCart.Entities;
using ShoppingCart.Entities.ShoppingRules;

namespace Tests.Entities;

public class SameOfMultipleProductShoppingRuleTests
{
    private readonly Product[] _products =
    [
        new Product("a", 50),
        new Product("b", 30),
        new Product("c", 20),
        new Product("d", 15)
    ];

    private readonly IShoppingRule[] _shoppingRules =
    [
        new SameOfMultipleProductShoppingRule("a", 3, 130),
        new SameOfMultipleProductShoppingRule("b", 2, 45),
    ];

    [TestCase(new string[] { }, 0)]
    [TestCase(new[] { "a" }, 50)]
    [TestCase(new[] { "a", "a" }, 100)]
    [TestCase(new[] { "a", "a", "a" }, 130)]
    [TestCase(new[] { "a", "a", "a", "a" }, 180)]
    [TestCase(new[] { "b" }, 30)]
    [TestCase(new[] { "b", "b" }, 45)]
    [TestCase(new[] { "b", "b", "b" }, 75)]
    [TestCase(new[] { "a", "a", "a", "a", "a", "a", "b", "b", "b", "b" }, 350)]
    [TestCase(new[] { "a", "a", "a", "a", "a", "a", "b", "b", "b", "b", "a", "b" }, 430)]
    [TestCase(new[] { "a", "b", "c", "d" }, 115)]
    [TestCase(new[] { "a", "b", "a", "c", "a", "d" }, 195)]
    [TestCase(new[] { "c", "d" }, 35)]
    [TestCase(new[] { "c", "d", "c", "d" }, 70)]
    [TestCase(new[] { "c", "d", "c", "d", "c", "d" }, 105)]
    public void NoShoppingRulesApply(string[] skus, float expectedTotalPrice)
    {
        Cart cart = new Cart(_shoppingRules);
        foreach (string sku in skus)
        {
            Product matchingProduct = _products.First(p => p.Sku == sku);
            cart.AddProduct(matchingProduct);
        }
        
        Assert.That(cart.Products.Count, Is.EqualTo(skus.Length));
        Assert.That(cart.TotalPrice, Is.EqualTo(expectedTotalPrice));
    }
}
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

    [TestCase("", 0)]
    [TestCase("a", 50)]
    [TestCase("aa", 100)]
    [TestCase("aaa", 130)]
    [TestCase("aaaa", 180)]
    [TestCase("b", 30)]
    [TestCase("bb", 45)]
    [TestCase("bbb", 75)]
    [TestCase("aaaaaabbbb", 350)]
    [TestCase("aaaaaabbbbab", 430)]
    [TestCase("abcd", 115)]
    [TestCase("abacad", 195)]
    [TestCase("cd", 35)]
    [TestCase("cdcd", 70)]
    [TestCase("cdcdcd", 105)]
    public void NoShoppingRulesApply(string skus, float expectedTotalPrice)
    {
        Cart cart = new Cart(_shoppingRules);
        foreach (char sku in skus)
        {
            Product matchingProduct = _products.First(p => p.Sku == sku.ToString());
            cart.AddProduct(matchingProduct);
        }
        
        Assert.That(cart.Products.Count, Is.EqualTo(skus.Length));
        Assert.That(cart.TotalPrice, Is.EqualTo(expectedTotalPrice));
    }
}
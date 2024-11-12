using ShoppingCart.Entities;
using ShoppingCart.Entities.ShoppingRules;

namespace Tests.Entities;

[TestFixture]
public class SameOfMultipleProductShoppingRuleTests
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
    ];

    [TestCase("", 0)]
    [TestCase("a", 50)]
    [TestCase("ab", 80)]
    [TestCase("cdba", 115)]

    [TestCase("aa", 100)]
    [TestCase("aaa", 130)]
    [TestCase("aaaa", 180)]
    [TestCase("aaaaa", 230)]
    [TestCase("aaaaaa", 260)]

    [TestCase("aaab", 160)]
    [TestCase("aaabb", 175)]
    [TestCase("aaabbd", 190)]
    [TestCase("dababa", 190)]
    // todo-at: test requests are a little different -- consider if needs changing:
    // - price is before skus
    // - skus are capitalized
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

    [Test]
    public void TestIncremental()
    {
        Checkout checkout = new Checkout(_shoppingRules);
        Assert.That(checkout.Products.Count, Is.EqualTo(0));
        Assert.That(checkout.TotalPrice, Is.EqualTo(0));

        checkout.Scan(GetProduct('a'));
        Assert.That(checkout.Products.Count, Is.EqualTo(1));
        Assert.That(checkout.TotalPrice, Is.EqualTo(50));

        checkout.Scan(GetProduct('b'));
        Assert.That(checkout.Products.Count, Is.EqualTo(2));
        Assert.That(checkout.TotalPrice, Is.EqualTo(80));

        checkout.Scan(GetProduct('a'));
        Assert.That(checkout.Products.Count, Is.EqualTo(3));
        Assert.That(checkout.TotalPrice, Is.EqualTo(130));

        checkout.Scan(GetProduct('a'));
        Assert.That(checkout.Products.Count, Is.EqualTo(4));
        Assert.That(checkout.TotalPrice, Is.EqualTo(160));

        checkout.Scan(GetProduct('b'));
        Assert.That(checkout.Products.Count, Is.EqualTo(5));
        Assert.That(checkout.TotalPrice, Is.EqualTo(175));
    }
    
    private Product GetProduct(char sku) =>
        _products.First(p => p.Sku == sku.ToString());
}
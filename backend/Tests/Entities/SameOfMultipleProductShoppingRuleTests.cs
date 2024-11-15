using ShoppingCart.Entities;

namespace Tests.Entities;

[TestFixture]
public class SameOfMultipleProductShoppingRuleTests
{
    private readonly DataHelper _dataHelper = new();
    
    [TestCase(0, "")]
    [TestCase(50, "a")]
    [TestCase(80, "ab")]
    [TestCase(115, "cdba")]

    [TestCase(100, "aa")]
    [TestCase(130, "aaa")]
    [TestCase(180, "aaaa")]
    [TestCase(150, "aaaaa")]
    [TestCase(200, "aaaaaa")]
    [TestCase(280, "aaaaaaaa")]

    [TestCase(160, "aaab")]
    [TestCase(175, "aaabb")]
    [TestCase(190, "aaabbd")]
    [TestCase(190, "dababa")]
    
    public void TestTotals(decimal expectedTotalPrice, string skus)
    {
        Checkout checkout = new(_dataHelper.BuildRulesManager());
        foreach (char sku in skus)
        {
            Product matchingProduct = _dataHelper.GetProduct(sku);
            checkout.Scan(matchingProduct);
        }
        
        Assert.That(checkout.Products.Count, Is.EqualTo(skus.Length));
        Assert.That(checkout.TotalPrice, Is.EqualTo(expectedTotalPrice));
    }

    [Test]
    public void TestIncremental()
    {
        Checkout checkout = new Checkout(_dataHelper.BuildRulesManager());
        Assert.That(checkout.Products.Count, Is.EqualTo(0));
        Assert.That(checkout.TotalPrice, Is.EqualTo(0));

        checkout.Scan(_dataHelper.GetProduct('a'));
        Assert.That(checkout.Products.Count, Is.EqualTo(1));
        Assert.That(checkout.TotalPrice, Is.EqualTo(50));

        checkout.Scan(_dataHelper.GetProduct('b'));
        Assert.That(checkout.Products.Count, Is.EqualTo(2));
        Assert.That(checkout.TotalPrice, Is.EqualTo(80));

        checkout.Scan(_dataHelper.GetProduct('a'));
        Assert.That(checkout.Products.Count, Is.EqualTo(3));
        Assert.That(checkout.TotalPrice, Is.EqualTo(130));

        checkout.Scan(_dataHelper.GetProduct('a'));
        Assert.That(checkout.Products.Count, Is.EqualTo(4));
        Assert.That(checkout.TotalPrice, Is.EqualTo(160));

        checkout.Scan(_dataHelper.GetProduct('b'));
        Assert.That(checkout.Products.Count, Is.EqualTo(5));
        Assert.That(checkout.TotalPrice, Is.EqualTo(175));
    }
}
using ShoppingCart.Entities;

namespace Tests.Entities;

[TestFixture]
public class SameOfMultipleProductShoppingRuleTests
{
    private readonly DataHelper _dataHelper = new();
    
    [TestCase(0, 0, "")]
    [TestCase(50, 50, "a")]
    [TestCase(80, 80, "ab")]
    [TestCase(115, 115, "cdba")]

    [TestCase(100, 100, "aa")]
    [TestCase(150, 130, "aaa")]
    [TestCase(200, 180, "aaaa")]
    [TestCase(250, 150, "aaaaa")]
    [TestCase(300, 200, "aaaaaa")]
    [TestCase(400, 280, "aaaaaaaa")]

    [TestCase(180, 160, "aaab")]
    [TestCase(210, 175, "aaabb")]
    [TestCase(225, 190, "aaabbd")]
    [TestCase(225, 190, "dababa")]
    
    public void TestTotals(decimal expectedTotalPrice, decimal expectedSpecialTotalPrice, string skus)
    {
        Checkout checkout = new(_dataHelper.BuildRulesManager());
        foreach (char sku in skus)
        {
            Product matchingProduct = _dataHelper.GetProduct(sku);
            checkout.Scan(matchingProduct);
        }
        
        Assert.That(checkout.CheckoutProducts.Count, Is.EqualTo(skus.Length));
        Assert.That(checkout.TotalPrice, Is.EqualTo(expectedTotalPrice));
        Assert.That(checkout.SpecialTotalPrice, Is.EqualTo(expectedSpecialTotalPrice));
    }

    [Test]
    public void TestIncremental()
    {
        Checkout checkout = new Checkout(_dataHelper.BuildRulesManager());
        Assert.That(checkout.CheckoutProducts.Count, Is.EqualTo(0));
        Assert.That(checkout.TotalPrice, Is.EqualTo(0));
        Assert.That(checkout.SpecialTotalPrice, Is.EqualTo(0));

        checkout.Scan(_dataHelper.GetProduct('a'));
        Assert.That(checkout.CheckoutProducts.Count, Is.EqualTo(1));
        Assert.That(checkout.TotalPrice, Is.EqualTo(50));
        Assert.That(checkout.SpecialTotalPrice, Is.EqualTo(50));

        checkout.Scan(_dataHelper.GetProduct('b'));
        Assert.That(checkout.CheckoutProducts.Count, Is.EqualTo(2));
        Assert.That(checkout.TotalPrice, Is.EqualTo(80));
        Assert.That(checkout.SpecialTotalPrice, Is.EqualTo(80));

        checkout.Scan(_dataHelper.GetProduct('a'));
        Assert.That(checkout.CheckoutProducts.Count, Is.EqualTo(3));
        Assert.That(checkout.TotalPrice, Is.EqualTo(130));
        Assert.That(checkout.SpecialTotalPrice, Is.EqualTo(130));

        checkout.Scan(_dataHelper.GetProduct('a'));
        Assert.That(checkout.CheckoutProducts.Count, Is.EqualTo(4));
        Assert.That(checkout.TotalPrice, Is.EqualTo(180));
        Assert.That(checkout.SpecialTotalPrice, Is.EqualTo(160));

        checkout.Scan(_dataHelper.GetProduct('b'));
        Assert.That(checkout.CheckoutProducts.Count, Is.EqualTo(5));
        Assert.That(checkout.TotalPrice, Is.EqualTo(210));
        Assert.That(checkout.SpecialTotalPrice, Is.EqualTo(175));
    }
}
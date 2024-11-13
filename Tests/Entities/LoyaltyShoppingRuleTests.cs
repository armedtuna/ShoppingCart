// using ShoppingCart.Entities;
// using ShoppingCart.Entities.ShoppingRules;
//
// namespace Tests.Entities;
//
// [TestFixture]
// public class LoyaltyShoppingRuleTests
// {
//     private readonly DataHelper _dataHelper = new();
//
//     private readonly IShoppingRule[] _shoppingRules =
//     [
//         new LoyaltyShoppingRule(new string[] { "a", "b", "c" }, 3),
//     ];
//     
//     [TestCase("abcd", 97 + 15)]
//     [TestCase("a", 48.5)]
//     [TestCase("b", 29.1)]
//     public void TestTotals(string skus, decimal expectedTotalPrice)
//     {
//         Checkout checkout = new Checkout(_shoppingRules);
//         foreach (char sku in skus)
//         {
//             Product matchingProduct = _dataHelper.GetProduct(sku);
//             checkout.Scan(matchingProduct);
//         }
//         
//         Assert.That(checkout.Products.Count, Is.EqualTo(skus.Length));
//         Assert.That(checkout.TotalPrice, Is.EqualTo(expectedTotalPrice));
//     }
// }

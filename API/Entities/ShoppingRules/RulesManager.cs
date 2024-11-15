namespace ShoppingCart.Entities.ShoppingRules;

public class RulesManager(IEnumerable<IShoppingRule>? shoppingRules)
{
    public IShoppingRule[] ShoppingRules { get; } = shoppingRules?.ToArray() ?? [];

    public CheckoutProduct[] ApplyShoppingRules(IEnumerable<Product> products)
    {
        Product[] productsArray = products.ToArray();
        
        List<IShoppingRule> matchingRules = [];
        foreach (IShoppingRule rule in ShoppingRules)
        {
            bool isApplicable = rule.IsApplicable(productsArray);
            if (isApplicable)
            {
                matchingRules.Add(rule);
            }
        }

        CheckoutProduct[] checkoutProducts = BuildCheckoutProducts(productsArray);
        CalculateSpecialPrices(matchingRules, checkoutProducts);

        return checkoutProducts;
    }

    private static CheckoutProduct[] BuildCheckoutProducts(Product[] products)
    {
        var checkoutProducts = new CheckoutProduct[products.Length];
        for (var index = 0; index < products.Length; index++)
        {
            checkoutProducts[index] = new CheckoutProduct(products[index]);
        }

        return checkoutProducts;
    }

    private static void CalculateSpecialPrices(List<IShoppingRule> matchingRules, CheckoutProduct[] checkoutProducts)
    {
        matchingRules.Sort((rule1, rule2) => rule2.Quantity.CompareTo(rule1.Quantity));
        foreach (IShoppingRule shoppingRule in matchingRules)
        {
            shoppingRule.CalculateSpecialPrice(checkoutProducts);
        }
    }
}
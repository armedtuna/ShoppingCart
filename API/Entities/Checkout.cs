using ShoppingCart.Entities.ShoppingRules;

namespace ShoppingCart.Entities;

// todo-at: validation? for example if user is allowed to manually enter the quantity. also where would stock levels be checked?
public class Checkout(IEnumerable<IShoppingRule>? shoppingRules)
{
    // todo-at: should these properties be exposed, perhaps for reporting purposes? out-of-scope? different objects for that?
    public List<Product> Products { get; } = [];
    
    // todo-at: could rules depend on who the cart belongs to? for example a customer with / without a loyalty card?
    public IShoppingRule[] ShoppingRules { get; } = shoppingRules?.ToArray() ?? [];

    public decimal TotalPrice { get; private set; } = 0;

    public void Scan(Product product)
    {
        Products.Add(product);
        
        List<CheckoutProduct> checkoutProducts = new();
        foreach (Product p in Products)
        {
            CheckoutProduct cp = new(p.Sku, p.UnitPrice);
            checkoutProducts.Add(cp);
        }
        
        List<IShoppingRule> matchingRules = new();
        // todo-at: should a "rule manager" class be extracted here?
        foreach (IShoppingRule rule in ShoppingRules)
        {
            bool isApplicable = rule.IsApplicable(checkoutProducts);
            if (isApplicable)
            {
                matchingRules.Add(rule);
            }
        }

        // todo-at: this should be descending order -- how to test all this? seems like an extract class might be helpful?
        matchingRules.Sort((rule1, rule2) => rule2.Quantity.CompareTo(rule1.Quantity));
        foreach (IShoppingRule shoppingRule in matchingRules)
        {
            shoppingRule.CalculateSpecialPrice(checkoutProducts);
        }
        
        CalculateTotalPrice(checkoutProducts);
    }

    private void CalculateTotalPrice(IEnumerable<CheckoutProduct> products)
    {
        decimal totalPrice = 0;
        foreach (CheckoutProduct product in products)
        {
            totalPrice += product.Price;
        }
        
        TotalPrice = decimal.Round(totalPrice, 2);
    }
}

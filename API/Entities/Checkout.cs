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
        // don't re-use the existing product, but create a new one since the special price is adjusted by the rule
        // on the product, and the object instances have to be unique.
        Product productCopy = product.Clone();
        Products.Add(productCopy);
        foreach (IShoppingRule rule in ShoppingRules)
        {
            bool ruleIsSatisfied = rule.CalculateSpecialPrice(Products);
        }
        
        CalculateTotalPrice();
    }

    private void CalculateTotalPrice()
    {
        decimal totalPrice = 0;
        foreach (Product product in Products)
        {
            if (product.SpecialPrice.HasValue)
            {
                totalPrice += product.SpecialPrice.Value;
            }
            else
            {
                totalPrice += product.UnitPrice;
            }
        }
        
        TotalPrice = decimal.Round(totalPrice, 2);
    }
}

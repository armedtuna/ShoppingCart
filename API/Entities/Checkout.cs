using ShoppingCart.Entities.ShoppingRules;

namespace ShoppingCart.Entities;

// todo-at: validation? for example if user is allowed to manually enter the quantity. also where would stock levels be checked?
public class Checkout(IEnumerable<IShoppingRule>? shoppingRules)
{
    // todo-at: should these properties be exposed, perhaps for reporting purposes? out-of-scope? different objects for that?
    public List<Product> Products { get; } = [];
    
    // todo-at: could rules depend on who the cart belongs to? for example a customer with / without a loyalty card?
    public IShoppingRule[] ShoppingRules { get; } = shoppingRules?.ToArray() ?? [];

    public float TotalPrice { get; private set; } = 0;

    public void Scan(Product product)
    {
        // todo-at: future improvement don't re-use the existing product, but create a new one and replace it where
        // necessary when changing the special price.
        // since the special price is adjusted by the rule, the object instances have to be unique.
        Product productCopy = product.Clone();
        Products.Add(productCopy);
        foreach (var rule in ShoppingRules)
        {
            //(bool ruleApplied, float rulePrice) = rule.CalculatePrice(Products);
            bool ruleIsSatisfied = rule.CalculateSpecialPrice(Products);
        }
        
        CalculateTotalPrice();
    }

    private void CalculateTotalPrice()
    {
        float totalPrice = 0;
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
        
        TotalPrice = totalPrice;
    }
}

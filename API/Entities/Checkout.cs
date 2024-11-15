using ShoppingCart.Entities.ShoppingRules;

namespace ShoppingCart.Entities;

// todo-at: validation? for example if user is allowed to manually enter the quantity. also where would stock levels be checked?
public class Checkout(RulesManager rulesManager)
{
    // todo-at: should these properties be exposed, perhaps for reporting purposes? out-of-scope? different objects for that?
    public List<Product> Products { get; } = [];

    public decimal TotalPrice { get; private set; } = 0;

    public void Scan(Product product)
    {
        Products.Add(product);
        CheckoutProduct[] checkoutProducts = rulesManager.ApplyShoppingRules(Products);
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

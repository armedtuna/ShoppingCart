using ShoppingCart.Entities;
using ShoppingCart.Entities.ShoppingRules;

namespace ShoppingCart.Data;

public class ExampleData
{
    public static Checkout BuildCheckout()
    {
        IShoppingRule[] shoppingRules = BuildShoppingRules();
        RulesManager rulesManager = new RulesManager(shoppingRules);
        return new Checkout(rulesManager);
    }

    private static IShoppingRule[] BuildShoppingRules()
    {
        return
        [
            new SameOfMultipleProductShoppingRule("A", 3, 130),
            new SameOfMultipleProductShoppingRule("B", 2, 45),
            new SameOfMultipleProductShoppingRule("A", 5, 150)
        ];
    }

    public static Product[] BuildProducts()
    {
        return
        [
            new Product("A", 50),
            new Product("B", 30),
            new Product("C", 20),
            new Product("D", 15)
        ];
    }
}

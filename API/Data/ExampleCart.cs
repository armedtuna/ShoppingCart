using ShoppingCart.Entities;
using ShoppingCart.Entities.ShoppingRules;

namespace ShoppingCart.Data;

public class ExampleCart
{
    public static Checkout Build()
    {
        IShoppingRule[] shoppingRules = ExampleShoppingRules.Build();
        return new Checkout(shoppingRules);
    }
}

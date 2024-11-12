using ShoppingCart.Entities;
using ShoppingCart.Entities.ShoppingRules;

namespace ShoppingCart.Data;

public class ExampleCart
{
    public static Cart Build()
    {
        IShoppingRule[] shoppingRules = ExampleShoppingRules.Build();
        return new Cart(shoppingRules);
    }
}

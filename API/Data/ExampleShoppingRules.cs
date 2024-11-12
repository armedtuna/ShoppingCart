using ShoppingCart.Entities.ShoppingRules;

namespace ShoppingCart.Data;

public class ExampleShoppingRules
{
    public static IShoppingRule[] Build()
    {
        return
        [
            new SameOfMultipleProductShoppingRule("A", 3, 130),
            new SameOfMultipleProductShoppingRule("B", 2, 45)
        ];
    }
}

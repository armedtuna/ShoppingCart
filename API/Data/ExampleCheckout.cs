using ShoppingCart.Entities;
using ShoppingCart.Entities.ShoppingRules;

namespace ShoppingCart.Data;

// todo-at: change this to a builder class perhaps under models? it doesn't really belong under Data namespace
public class ExampleCheckout
{
    public static Checkout Build()
    {
        IShoppingRule[] shoppingRules = ExampleShoppingRules.Build();
        return new Checkout(shoppingRules);
    }

    public static Checkout Build(params IShoppingRule[] shoppingRules) =>
        new(shoppingRules);

    public static Checkout BuildSameOfMultipleProduct() =>
        Build(BuildSameOfMultipleShoppingRules());

    // // should be some interesting results, if the discount rule is applied first? :)
    // public static Checkout BuildComplex() =>
    //     Build(
    //     [
    //         ..BuildSameOfMultipleShoppingRules(),
    //         new LoyaltyShoppingRule([], 3)
    //     ]);

    private static IShoppingRule[] BuildSameOfMultipleShoppingRules() =>
    [
        new SameOfMultipleProductShoppingRule("A", 3, 130),
        new SameOfMultipleProductShoppingRule("B", 2, 45)
    ];
}

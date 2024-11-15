namespace ShoppingCart.Entities.ShoppingRules;

// todo-at: what if rules are added that apply to multiple products, and then a product is covered by multiple rules?
// tl;dr are rules mutually exclusive? i know at this stage there's no overlap, but it's good to at least consider
// this for the future.

// todo-at: i imagine that most products won't have rule overlap, but what about loyalty systems? then every product
// already has a special price? so should those consider the best special price? there could be a benefit of showing
// the user which special price applied? then again when considering the best special price, does one special price
// invalidate another? i guess perhaps the rules need a setting for whether they're inclusive or exclusive? or perhaps
// the rule creation system needs to block such scenarios from occurring in the first place? or perhaps a bit of both?
// imagine this scenario:
// - loyalty points get a lower price
// - today only if you buy two you get an even better price
public interface IShoppingRule
{
    public string Sku { get; }
    
    public int Quantity { get; }

    bool IsApplicable(IEnumerable<Product> products);

    void CalculateSpecialPrice(IEnumerable<CheckoutProduct> products);
}

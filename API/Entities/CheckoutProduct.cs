namespace ShoppingCart.Entities;

public class CheckoutProduct(string sku, decimal price)
{
    // todo-at: should this just hold a copy of the product entity?
    public string Sku { get; } = sku;

    public decimal Price { get; set; } = price;

    // todo-at: keep or remove or evolve -- for now it's for debug purposes
    public string? SpecialPriceName { get; set; } = null;

    public bool HasSpecialPrice =>
        SpecialPriceName != null;
}

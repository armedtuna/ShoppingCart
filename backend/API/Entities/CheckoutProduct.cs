namespace ShoppingCart.Entities;

public class CheckoutProduct(string sku, decimal unitPrice)
{
    // todo-at: should this just hold a copy of the product entity?
    public string Sku { get; } = sku;

    public decimal UnitPrice { get; set; } = unitPrice;

    public decimal? SpecialPrice { get; set; } = null;

    // todo-at: extract the two special fields into their own class?
    // todo-at: keep as-is or evolve?
    public string? SpecialPriceName { get; set; } = null;

    public bool HasSpecialPrice =>
        SpecialPriceName != null;

    public CheckoutProduct(Product product)
        : this(product.Sku, product.UnitPrice) { }
}

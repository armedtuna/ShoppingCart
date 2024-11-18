namespace ShoppingCart.Entities;

public class CheckoutProduct(string sku, decimal price)
{
    // todo-at: should this just hold a copy of the product entity?
    public string Sku { get; } = sku;

    public decimal Price { get; set; } = price;

    // todo-at: extract the two special fields into their own class?
    // todo-at: keep as-is or evolve?
    public string? SpecialPriceName { get; set; } = null;

    public bool HasSpecialPrice =>
        SpecialPriceName != null;

    public CheckoutProduct(Product product)
        : this(product.Sku, product.UnitPrice) { }
}

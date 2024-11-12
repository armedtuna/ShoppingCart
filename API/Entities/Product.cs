namespace ShoppingCart.Entities;

public class Product(string sku, decimal unitPrice)
{
    // todo-at: should product SKU be case-sensitive? at the moment it is (case-sensitive)
    public string Sku { get; set; } = sku;

    public decimal UnitPrice { get; set; } = unitPrice;

    // todo-at: should a class be extracted here? perhaps
    public string? SpecialPriceName { get; set; } = null;
    public decimal? SpecialPrice { get; set; } = null;

    public Product Clone() =>
        (Product)this.MemberwiseClone();
}

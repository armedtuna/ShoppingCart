namespace ShoppingCart.Entities;

public class Product(string sku, decimal unitPrice)
{
    // todo-at: should product SKU be case-sensitive? at the moment it is (case-sensitive)
    public string Sku { get; } = sku;

    public decimal UnitPrice { get; } = unitPrice;
}

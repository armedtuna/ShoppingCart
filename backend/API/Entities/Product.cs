namespace ShoppingCart.Entities;

public class Product(string sku, decimal unitPrice)
    : ScanProduct(sku)
{
    public decimal UnitPrice { get; } = unitPrice;
}

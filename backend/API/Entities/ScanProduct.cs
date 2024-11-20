using System.ComponentModel.DataAnnotations;

namespace ShoppingCart.Entities;

public class ScanProduct(string sku)
{
    // todo-at: should product SKU be case-sensitive? at the moment it is (case-sensitive)
    public string Sku { get; } = sku;
}

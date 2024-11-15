namespace ShoppingCart.Entities.ShoppingRules;

public class SameOfMultipleProductShoppingRule(string sku, int quantity, decimal specialPrice) : IShoppingRule
{
    public string Sku { get; } = sku;

    public int Quantity { get; } = quantity;

    private decimal SpecialPrice { get; } = specialPrice;

    // for the moment, assume that all rules can apply to a product more than once
    private bool Inclusive { get; set; } = true;

    // todo-at: tests
    // todo-at: something about this design feels off / wrong
    // - should this receive normal products?
    public bool IsApplicable(IEnumerable<Product> products)
    {
        Product[] matchingProducts = products
            .Where(p => p.Sku == Sku)
            .ToArray();

        if (matchingProducts.Length == 0)
        {
            return false;
        }
        
        // - 2 matches: no match
        // - 3, or 4 matches: offer x 1
        // - 6, or 8 matches: offer x 2
        int quantityToAdjust = matchingProducts.Length / Quantity * Quantity;
        return quantityToAdjust != 0;
    }

    // todo-at: tests
    public void CalculateSpecialPrice(IEnumerable<CheckoutProduct> products)
    {
        CheckoutProduct[] matchingProducts = products
            .Where(p => p.Sku == Sku &&
                        !p.HasSpecialPrice)
            .ToArray();

        if (matchingProducts.Length == 0)
        {
            return;
        }
        
        // - 2 matches: no match
        // - 3, or 4 matches: offer x 1
        // - 6, or 8 matches: offer x 2
        int quantityToAdjust = matchingProducts.Length / Quantity * Quantity;
        if (quantityToAdjust == 0)
        {
            return;
        }

        for (int i = 0; i < quantityToAdjust; i++)
        {
            CheckoutProduct matchingProduct = matchingProducts[i];
            matchingProduct.SpecialPriceName = GetType().ToString(); // todo-at: replace or append?
            if (i % Quantity == 0)
            {
                matchingProduct.Price = SpecialPrice;
            }
            else
            {
                matchingProduct.Price = 0;
            }
        }
    }
}

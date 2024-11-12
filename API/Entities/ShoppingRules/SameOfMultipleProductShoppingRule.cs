namespace ShoppingCart.Entities.ShoppingRules;

public class SameOfMultipleProductShoppingRule(string sku, int quantity, decimal specialPrice) : IShoppingRule
{
    private string Sku { get; set; } = sku;

    private int Quantity { get; set; } = quantity;

    private decimal SpecialPrice { get; set; } = specialPrice;

    // for the moment, assume that all rules can apply to a product more than once
    private bool Inclusive { get; set; } = true;

    public bool CalculateSpecialPrice(IEnumerable<Product> products)
    {
        Product[] matchingProducts = products
            .Where(p => p.Sku == Sku /*&&
                        !p.SpecialPrice.HasValue*/)
            .ToArray();

        if (matchingProducts.Length == 0)
        {
            return false;
        }
        
        // - 2 matches: no match
        // - 3, or 4 matches: offer x 1
        // - 6, or 8 matches: offer x 2
        int quantityToAdjust = matchingProducts.Length / Quantity * Quantity;
        if (quantityToAdjust == 0)
        {
            return false;
        }

        for (int i = 0; i < quantityToAdjust; i++)
        {
            Product matchingProduct = matchingProducts[i];
            // if (SpecialPrice > matchingProduct.SpecialPrice)
            // {
            //     continue;
            // }

            matchingProduct.SpecialPriceName = GetType().ToString(); // todo-at: replace or append?
            if (i % Quantity == 0)
            {
                matchingProduct.SpecialPrice = SpecialPrice;
            }
            else
            {
                matchingProduct.SpecialPrice = 0;
            }
        }

        return true;
    }
}

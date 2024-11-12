namespace ShoppingCart.Entities.ShoppingRules;

public class SameOfMultipleProductShoppingRule(string sku, int quantity, float specialPrice) : IShoppingRule
{
    private string Sku { get; set; } = sku;

    private int Quantity { get; set; } = quantity;

    private float SpecialPrice { get; set; } = specialPrice;

    // for the moment, assume that all rules can apply to a product more than once
    private bool Inclusive { get; set; } = true;

    // todo-at: tests
    public bool CalculateSpecialPrice(IEnumerable<Product> products)
    {
        Product[] matchingProducts = products
            .Where(p => p.Sku == Sku &&
                        !p.SpecialPrice.HasValue)
            .ToArray();

        if (matchingProducts.Length == 0)
        {
            return false;
        }
        
        // todo-at: need to cater for multiples, for example with rule for 3 of sku
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
            if (SpecialPrice > matchingProduct.SpecialPrice)
            {
                continue;
            }

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

    // public (bool, float) CalculatePrice(IEnumerable<Product> products)
    // {
    //     Product[] matchingProducts = products.Where(p => p.Sku == Sku)
    //         .ToArray();
    //
    //     if (matchingProducts.Length == 0)
    //     {
    //         return (false, 0);
    //     }
    //     
    //     float matchesPrice = 0;
    //     float remainderPrice = 0;
    //     // todo-at: need to cater for multiples, for example with rule for 3 of sku
    //     // - 2 matches: no match
    //     // - 3, or 4 matches: offer x 1
    //     // - 6, or 8 matches: offer x 2
    //     int matches = matchingProducts.Length / Quantity;
    //     matchesPrice = matches * SpecialPrice;
    //     float productPrice = matchingProducts.First().UnitPrice;
    //     remainderPrice = (matchingProducts.Length - matches) * productPrice;
    //
    //     return (true, matchesPrice + remainderPrice);
    // }
}

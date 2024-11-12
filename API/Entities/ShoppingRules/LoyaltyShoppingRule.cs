namespace ShoppingCart.Entities.ShoppingRules;

public class LoyaltyShoppingRule(string[] skus, byte percentageDiscount) : IShoppingRule
{
    private string[] Skus { get; set; } = skus;

    private byte PercentageDiscount { get; set; } = percentageDiscount;
    
    public bool CalculateSpecialPrice(IEnumerable<Product> products)
    {
        Product[] matchingProducts = products
            .Where(p => Skus.Contains(p.Sku))
            .ToArray();

        if (matchingProducts.Length == 0)
        {
            return false;
        }
        
        foreach (Product product in matchingProducts)
        {
            string name = GetType().ToString();
            // this isn't the best to ensure that a percentage discount isn't applied multiple times.
            if (product.SpecialPrice.HasValue && product.SpecialPriceName != name)
            {
                product.SpecialPrice -= product.SpecialPrice.Value * PercentageDiscount / 100;
            }
            else
            {
                product.SpecialPrice = product.UnitPrice - product.UnitPrice * PercentageDiscount / 100;
            }

            product.SpecialPriceName = name;
        }

        return true;
    }
}
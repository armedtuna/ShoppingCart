using ShoppingCart.Entities.ShoppingRules;

namespace ShoppingCart.Entities;

public class Checkout(RulesManager rulesManager)
{
    private readonly List<Product> _products = [];
    private CheckoutProduct[] _checkoutProducts = [];

    public IReadOnlyCollection<CheckoutProduct> CheckoutProducts => _checkoutProducts.AsReadOnly();

    public decimal TotalPrice { get; private set; } = 0;
    public decimal SpecialTotalPrice { get; private set; } = 0;

    public void Scan(Product product)
    {
        _products.Add(product);
        CalculateTotals();
    }

    private void CalculateTotals()
    {
        _checkoutProducts = rulesManager.ApplyShoppingRules(_products);
        CalculateTotalPrice(_checkoutProducts);
    }

    public bool Delete(string sku)
    {
        Product? product = _products.FirstOrDefault(p => p.Sku == sku);
        if (product == null)
        {
            return false;
        }
        
        _products.Remove(product);
        CalculateTotals();
        return true;
    }

    private void CalculateTotalPrice(IEnumerable<CheckoutProduct> products)
    {
        decimal totalPrice = 0;
        decimal specialTotalPrice = 0;
        foreach (CheckoutProduct product in products)
        {
            totalPrice += product.UnitPrice;
            if (product.SpecialPrice.HasValue)
            {
                specialTotalPrice += product.SpecialPrice.Value;
            }
            else
            {
                specialTotalPrice += product.UnitPrice;
            }
        }
        
        TotalPrice = decimal.Round(totalPrice, 2);
        SpecialTotalPrice = decimal.Round(specialTotalPrice, 2);
    }
}

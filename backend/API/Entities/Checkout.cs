using ShoppingCart.Entities.ShoppingRules;

namespace ShoppingCart.Entities;

public class Checkout(RulesManager rulesManager)
{
    private readonly List<Product> _products = [];

    public decimal TotalPrice { get; private set; } = 0;

    public void Scan(Product product)
    {
        _products.Add(product);
        CheckoutProduct[] checkoutProducts = rulesManager.ApplyShoppingRules(_products);
        CalculateTotalPrice(checkoutProducts);
    }

    private void CalculateTotalPrice(IEnumerable<CheckoutProduct> products)
    {
        decimal totalPrice = 0;
        foreach (CheckoutProduct product in products)
        {
            totalPrice += product.Price;
        }
        
        TotalPrice = decimal.Round(totalPrice, 2);
    }

    public Product[] GetProducts() =>
        _products.ToArray();
}

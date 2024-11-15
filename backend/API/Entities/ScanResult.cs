namespace ShoppingCart.Entities;

public class ScanResult
{
    // todo-at: replace this with an HTTP status code
    public bool Success { get; set; }
    
    public string[] Errors { get; set; }
    
    public decimal TotalPrice { get; set; }
}
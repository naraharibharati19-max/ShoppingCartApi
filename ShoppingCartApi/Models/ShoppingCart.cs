namespace ShoppingCartApi.Models;

public class ShoppingCart
{
    public List<CartItem> Items { get; set; } = new List<CartItem>();
}
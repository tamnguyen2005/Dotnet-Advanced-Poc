namespace DDD.Models;

public class OrderItem
{
    public Guid ProductId { get; set; }
    public Money Price { get; set; }
    public int Quantity { get; set; }
}
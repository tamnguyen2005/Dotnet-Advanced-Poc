using DDD.Exceptions;

namespace DDD.Models;

public class Order
{
    public Guid Id { get; set; }

    public DateTime OrderDate { get; set; }
    public string Status { get; set; } = string.Empty;
    public Money Total { get;private set; } = Money.Zero("VND");
    private List<OrderItem> OrderItems { get; } = new();

    public void AddProduct(Guid productId, Money price, int quantity)
    {
        if (Status != "Pending") throw new DomainException("Không thể chỉnh sửa đơn hàng đã thanh toán !");
        var itemAmount = price.Multiply(quantity);
        OrderItems.Add(new OrderItem
        {
            ProductId = productId,
            Price = price,
            Quantity = quantity
        });
        Total += itemAmount;
    }
}
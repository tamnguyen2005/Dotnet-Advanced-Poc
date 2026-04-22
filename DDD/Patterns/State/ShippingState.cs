using DDD.Models;

namespace DDD.Patterns.State;

public class ShippingState : OrderState
{
    public override void Complete(Order order)
    {
        order.Status = "Completed";
    }
}
using DDD.Exceptions;
using DDD.Models;

namespace DDD.Patterns.State;

public abstract class OrderState
{
    public virtual void Confirm(Order order)
    {
        throw new DomainException($"Không thể xác nhận đơn hàng khi đang ở trạng thái {order.Status}");
    }

    public virtual void Ship(Order order)
    {
        throw new DomainException($"Không thể giao đơn hàng khi đang ở trạng thái {order.Status}");
    }

    public virtual void Complete(Order order)
    {
        throw new DomainException($"Không thể hoàn thành đơn hàng khi đang ở trạng thái {order.Status}");
    }

    public virtual void Cancel(Order order)
    {
        throw new DomainException($"Không thể hủy đơn hàng khi đang ở trạng thái {order.Status}");
    }
}
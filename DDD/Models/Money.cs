using DDD.Exceptions;

namespace DDD.Models;

public class Money : ValueObject
{
    private Money(decimal amount, string currency)
    {
        Amount = amount;
        Currency = currency;
    }

    public decimal Amount { get; }
    public string Currency { get; }

    public static Money Vie(decimal amount)
    {
        return new Money(amount, "VND");
    }

    public static Money Usd(decimal amount)
    {
        return new Money(amount, "USD");
    }

    public static Money operator +(Money a, Money b)
    {
        if (a.Currency != b.Currency) throw new DomainException("Không thể cộng 2 loại tiền tệ khác nhau !");

        return new Money(a.Amount + b.Amount, a.Currency);
    }

    public static Money Zero(string currency)
    {
        return new Money(0, currency);
    }

    public Money Multiply(int quantity)
    {
        return new Money(Amount * quantity, Currency);
    }
}
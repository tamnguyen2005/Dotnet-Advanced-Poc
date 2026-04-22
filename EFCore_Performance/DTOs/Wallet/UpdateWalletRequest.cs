namespace EFCore_Performance.DTOs.Wallet;

public class UpdateWalletRequest
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
}
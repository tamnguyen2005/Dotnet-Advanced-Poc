using System.ComponentModel.DataAnnotations;

namespace EFCore_Performance.DTOs.Wallet;

public class CreateWalletRequest
{
    [Required]
    public decimal Balance { get; set; }
}
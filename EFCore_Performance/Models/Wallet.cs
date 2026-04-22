using System.ComponentModel.DataAnnotations;

namespace EFCore_Performance.Models;

public class Wallet
{
    public Guid Id { get; set; }
    public decimal Balance { get; set; }
    [Timestamp]
    public byte[] RowVersion { get; set; }
}
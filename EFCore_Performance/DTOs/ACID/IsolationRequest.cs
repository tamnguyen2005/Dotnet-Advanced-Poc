using System.Transactions;

namespace EFCore_Performance.DTOs;

public class IsolationRequest
{
    public string Level { get; set; }
    public string AccountNumber { get; set; }
}
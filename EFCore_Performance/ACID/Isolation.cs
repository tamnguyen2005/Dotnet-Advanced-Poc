using System.Data;
using EFCore_Performance.Data;
using EFCore_Performance.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCore_Performance.ACID;

public class Isolation
{
    private readonly AppDbContext _context;
    private readonly IServiceScopeFactory _scopeFactory;
    public Isolation(AppDbContext context,IServiceScopeFactory scopeFactory)
    {
        _context = context;
        _scopeFactory = scopeFactory;
    }

    public async Task DemoTransactionLocking(IsolationLevel isolationLevel,string accountNumber)
    {
        using var trans = await _context.Database.BeginTransactionAsync(isolationLevel);
        try
        {
            var amount = await _context.Transactions.Where(t => t.AccountNumber == accountNumber)
                .SumAsync(t => t.Amount);
            Console.WriteLine($"[Luồng A] Đã chi tiêu: {amount}");
            _ = Task.Run(async () =>
            {
                await Task.Delay(1000);
                using var scope = _scopeFactory.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                Console.WriteLine("[Luồng B] Đang lén chèn thêm giao dịch 4,000,000đ...");
                dbContext.Transactions.Add(new Transaction()
                {
                    AccountNumber = accountNumber,
                    Amount = 4000000,
                    Id = Guid.NewGuid(),
                    Description = "Lorem ipsum dolor sit amet",
                    TransactionDate = DateTime.Now
                });
                await dbContext.SaveChangesAsync();
                Console.WriteLine("[Luồng B] CHÈN THÀNH CÔNG!");
            });
            amount=await _context.Transactions.Where(t => t.AccountNumber == accountNumber)
                .SumAsync(t => t.Amount);
            Console.WriteLine($"[Luồng A] Sau đoạn code của luồng B và số tiền bây giờ đang là {amount}");
            await trans.CommitAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            await trans.RollbackAsync();
        }
    }
    
}
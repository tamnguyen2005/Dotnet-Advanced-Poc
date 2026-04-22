using EFCore_Performance.Data;
using EFCore_Performance.DTOs.Wallet;
using EFCore_Performance.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCore_Performance.Controllers;
[ApiController]
[Route("api/[controller]/")]
public class OptimisticController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IServiceScopeFactory _scopeFactory;

    public OptimisticController(AppDbContext context,IServiceScopeFactory scopeFactory)
    {
        this._context = context;
        _scopeFactory = scopeFactory;
    }

    [HttpPost("CreateWallet")]

    public async Task<IActionResult> CreateWallet(CreateWalletRequest request)
    {
        var wallet = new Wallet()
        {
            Balance = request.Balance,
            Id = Guid.NewGuid()
        };
        await _context.Wallets.AddAsync(wallet);
        await _context.SaveChangesAsync();
        return Ok();
    }
    [HttpPost("UpdateWallet")]
    public async Task<IActionResult> DemoOptimisticLock(UpdateWalletRequest request)
    {
        var wallet = await _context.Wallets.FindAsync(request.Id);
        Console.WriteLine($"Ví tiền có mã {wallet?.Id} có số dư là {wallet?.Balance}");
        using (var scope=_scopeFactory.CreateScope())
        {
            Console.WriteLine("Luồng 2 bắt đầu nạp tiền !");
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var wallet1 =await  dbContext.Wallets.FindAsync(request.Id);
            wallet1.Balance += 100000; 
            dbContext.Wallets.Update(wallet1);
            await dbContext.SaveChangesAsync();
            Console.WriteLine("Luồng 2 nạp tiền hoàn tất !");
        }
        Console.WriteLine("Luồng 1 bắt đầu nạp tiền");
        try
        {
            wallet.Balance += request.Amount;
            _context.Wallets.Update(wallet);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            Console.WriteLine("Luồng 1 nạp tiền thất bại rồi huhu !");
        }
    
        return NoContent();
    }

    public async Task<IActionResult> UpdateWalletWithRetry(UpdateWalletRequest request)
    {
        int maxRetries = 3;
        int retryCount = 0;
        bool saveFailed;
        do
        {
            saveFailed = false;
            try
            {
                var wallet = await _context.Wallets.FindAsync(request.Id);
                wallet.Balance += request.Amount;
                _context.Wallets.Update(wallet);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                saveFailed = true;
                retryCount++;
                if (retryCount >= maxRetries)
                {
                    Console.WriteLine("Đã thử quá nhiều lần rồi !");
                    throw;
                }

                var entry = e.Entries.Single();
                await entry.ReloadAsync();
                Console.WriteLine($"Lần {retryCount} Xảy ra xung đột . Đang thử lại !");
            }
        } while (true);
    }
}
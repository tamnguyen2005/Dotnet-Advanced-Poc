using EFCore_Performance.Data;
using EFCore_Performance.DTOs;

namespace EFCore_Performance.ACID;

public class Atomacity
{
    private readonly AppDbContext _dbContext;
    public Atomacity(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Update(AtomacityRequest request)
    {
        using var transaction = await _dbContext.Database.BeginTransactionAsync();
        var trans= await _dbContext.Transactions.FindAsync(request.Id);
        if (trans == null)
        {
            throw new Exception("Giao dich không tồn tại");
        }
        trans.Amount= request.Amount;
        await _dbContext.SaveChangesAsync();
        await Task.Delay(20000);
        await transaction.CommitAsync();
    }
}
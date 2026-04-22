using System.Transactions;
using EFCore_Performance.ACID;
using EFCore_Performance.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace EFCore_Performance.Controllers;
[ApiController]
[Route("/api/[controller]")]
public class ACIDController : ControllerBase
{
    private readonly Atomacity _atomacity;
    private readonly Isolation _isolation;
    public ACIDController(Atomacity atomacity,Isolation isolation)
    {
        _atomacity = atomacity;
        _isolation = isolation;
    }
    [HttpPost("/Isolation")]
    public async Task<IActionResult> Isolation(IsolationRequest request)
    {
        var level = System.Data.IsolationLevel.ReadCommitted;
        switch (request.Level)
        {
            case "r": level = System.Data.IsolationLevel.RepeatableRead;
                break;
            case "s": level = System.Data.IsolationLevel.Serializable;
                break;
        }

        await _isolation.DemoTransactionLocking(level,request.AccountNumber);
        return NoContent();
    }
    [HttpPost("/Atomacity")]
    public async Task<IActionResult> Atomacity(AtomacityRequest request)
    {
        await _atomacity.Update(request);
        return NoContent();
    }
}
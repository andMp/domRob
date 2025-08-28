using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class TipController : ControllerBase
{
    private readonly EthereumService eth;
    public TipController(EthereumService service) => eth = service;

    [HttpGet("last")]
    public async Task<IActionResult> Last(int count = 10)
    {
        var tips = await eth.GetLastTipsAsync(count);
        return Ok(tips);
    }

    //[HttpGet("top")]
    //public async Task<IActionResult> Top(int top = 3)
    //{
    //    var tips = await eth.GetLastTipsAsync(500);
    //    var topList = tips
    //        .GroupBy(t => t.From)
    //        .Select(g => new { Address = g.Key, Total = g.Sum(x => (decimal)x.Amount / 1e18m) })
    //        .OrderByDescending(x => x.Total)
    //        .Take(top);
    //    return Ok(topList);
    //}

    [HttpGet("top")]
    public async Task<IActionResult> Top(int top = 3)
    {
        var topList = await eth.GetTopDonorsAsync(top);
        return Ok(topList);
    }

    [HttpGet("total")]
    public async Task<IActionResult> Total()
    {
        var total = await eth.GetTotalTipsAsync();
        return Ok(total);
    }

    [HttpGet("status")]
    public async Task<IActionResult> Status()
    {
        var paused = await eth.GetPausedAsync();
        var minTip = await eth.GetMinTipAsync();
        var cooldown = await eth.GetCooldownAsync();
        var owner = await eth.GetOwnerAsync();
        return Ok(new
        {
            Paused = paused,
            MinTip = (decimal)minTip / 1e18m,
            CooldownSec = cooldown,
            Owner = owner
        });
    }
}

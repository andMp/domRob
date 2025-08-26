using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class TipController : ControllerBase
{
    private readonly EthereumService ethService;
    public TipController(EthereumService service) => ethService = service;

    [HttpGet("last")]
    public async Task<IActionResult> LastTips(int count = 10)
    {
        var tips = await ethService.GetLastTipsAsync(count);
        return Ok(tips);
    }

    [HttpGet("top")]
    public async Task<IActionResult> Top(int top = 3)
    {
        var tips = await ethService.GetLastTipsAsync(1000); // можна змінити на потрібну кількість
        var topList = tips
            .GroupBy(t => t.From)
            .Select(g => new { Address = g.Key, Total = g.Sum(x => (decimal)x.Amount / 1_000_000_000_000_000_000m) })
            .OrderByDescending(x => x.Total)
            .Take(top);
        return Ok(topList);
    }

    [HttpGet("total")]
    public async Task<IActionResult> Total()
    {
        var total = await ethService.GetTotalTipsAsync();
        return Ok(total);
    }
}

using Metrics.App.Services;
using Microsoft.AspNetCore.Mvc;

namespace Metrics.App.Controllers;

[ApiController]
[Route("[controller]")]
public class DataController : ControllerBase
{
    private readonly IApiClient _apiClient;
    
    public DataController(IApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    [HttpGet]
    [Route("data")]
    public async Task<IActionResult> GetData()
    {
        await _apiClient.GetData();
        return Ok();
    }
    
    [HttpGet]
    [Route("calculate")]
    public async Task<IActionResult> Calculate()
    {
        var time = Random.Shared.Next(100, 2_000);
        await Task.Delay(time);
        return Ok();
    }
}


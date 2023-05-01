using Metrics.App.Services;
using Microsoft.AspNetCore.Mvc;

namespace Metrics.App.Controllers;

[ApiController]
[Route("[controller]")]
public class JobController : ControllerBase
{
    private readonly IJobService _jobService;
    private readonly IApiClient _apiClient;
    
    public JobController(
        IJobService jobService,
        IApiClient apiClient)
    {
        _jobService = jobService;
        _apiClient = apiClient;
    }
    
    [HttpPost]
    [Route("job1-bad")]
    public async Task<IActionResult> Job1Bad()
    {
        await _jobService.Job1Bad();
        return Ok();
    }
    
    [HttpPost]
    [Route("job1-good")]
    public async Task<IActionResult> Job1Good()
    {
        await _jobService.Job1Good();
        return Ok();
    }
    
    [HttpPost]
    [Route("job2-bad")]
    public async Task<IActionResult> Job2Bad()
    {
        await _jobService.Job2Bad();
        return Ok();
    }
    
    [HttpPost]
    [Route("job2-good")]
    public async Task<IActionResult> Foo()
    {
        await _jobService.Job2Good();
        return Ok();
    }
}


namespace Metrics.App.Services;

class JobService: IJobService
{
    private readonly object _sync;
    private int _requestId;
    private readonly SemaphoreSlim _semaphor;

    public JobService()
    {
        _sync = new object();
        _semaphor = new SemaphoreSlim(1, 1);
    }
    
    public Task Job1Bad()
    {
        Thread.Sleep(200);
        return Task.CompletedTask;
    }
    
    public async Task Job1Good()
    {
        await Task.Delay(200);
    }
    
    public Task Job2Bad()
    {
        lock (_sync)
        {
            if (_requestId % 100 == 0)
            {
                var s = 0;
                for (int i = 0; i < 10_000; i++)
                    s += i;
            }
        }

        Interlocked.Increment(ref _requestId);
        
        return Task.CompletedTask;
    }
    
    public async Task Job2Good()
    {
        if (_requestId % 100 == 0)
        {
            await _semaphor.WaitAsync();
            try
            {
                var s = 0;
                for (int i = 0; i < 10_000; i++)
                    s += i;
            }
            finally
            {
                _semaphor.Release();
            }
        }
        
        Interlocked.Increment(ref _requestId);
    }
}
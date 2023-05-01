namespace Metrics.App.Services;

public interface IJobService
{
    Task Job1Bad();
    Task Job1Good();
    Task Job2Bad();
    Task Job2Good();
}
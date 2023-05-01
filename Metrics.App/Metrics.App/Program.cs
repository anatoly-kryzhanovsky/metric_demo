using Metrics.App.Metrics;
using Metrics.App.Services;
using OpenTelemetry.Metrics;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<MetricHandler>();
builder.Services
    .AddHttpClient(ApiClient.ApiClientName)
    .AddHttpMessageHandler<MetricHandler>();

builder.Services.AddScoped<IApiClient, ApiClient>();
builder.Services.AddSingleton<IJobService, JobService>();

builder.Services
    .AddOpenTelemetry()
    .WithMetrics(x =>
    {
        x.AddEventCountersInstrumentation();
        x.AddHttpClientInstrumentation();
        x.AddRuntimeInstrumentation();
        x.AddAspNetCoreInstrumentation();
        x.AddProcessInstrumentation();
        x.AddPrometheusExporter();
        x.AddMeter(HttpClientMetrics.HttpClientMetricsName);
    });

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();
app.UseOpenTelemetryPrometheusScrapingEndpoint();

app.Run();
using System.Diagnostics;
using System.Net;
using Metrics.App.Metrics;

namespace Metrics.App.Services;

class MetricHandler : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var startTime = Stopwatch.GetTimestamp();
        
        var method = request.Method;
        var target = request.RequestUri.LocalPath;
        HttpStatusCode? statusCode = null;
        bool success = false;

        try
        {
            var response = await base.SendAsync(request, cancellationToken);
            statusCode = response.StatusCode;
            success = true;

            return response;
        }
        finally
        {
            var elapsed = Stopwatch.GetElapsedTime(startTime);
            HttpClientMetrics.RecordRequest(
                elapsed, 
                method.ToString(), 
                target, 
                statusCode == null ? null : (int)statusCode,
                success);
        }
    }

    protected override HttpResponseMessage Send(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var startTime = Stopwatch.GetTimestamp();
        
        var method = request.Method;
        var target = request.RequestUri.LocalPath;
        HttpStatusCode? statusCode = null;
        bool success = false;

        try
        {
            var response = base.Send(request, cancellationToken);
            statusCode = response.StatusCode;
            success = true;

            return response;
        }
        finally
        {
            var elapsed = Stopwatch.GetElapsedTime(startTime);
            HttpClientMetrics.RecordRequest(
                elapsed, 
                method.ToString(), 
                target, 
                statusCode == null ? null : (int)statusCode,
                success);
        }
    }
}
using System.Diagnostics.Metrics;

namespace Metrics.App.Metrics;

static class HttpClientMetrics
{
    public const string HttpClientMetricsName = "http-client";
    
    private const string HttpClientHistogramName = "http-client-histogram";
    private const string HttpMethodLabel = "http-method";
    private const string HttpTargetLabel = "http-target";
    private const string HttpResponseCodeLabel = "http-response-code";
    private const string HttpSuccessLabel = "http-response-success";
    private const string HttpSuccessTrueValue = "true";
    private const string HttpSuccessFalseValue = "false";
    
    private static readonly Meter Meter = new Meter(HttpClientMetricsName);
    
    private static readonly Histogram<long> HttpClientHistogram = Meter.CreateHistogram<long>(HttpClientHistogramName, "ms");

    public static void RecordRequest(
        TimeSpan elapsed,
        string method,
        string target,
        int? statusCode,
        bool success)
    {
        HttpClientMetrics.HttpClientHistogram.Record(
            (long)elapsed.TotalMilliseconds, 
            new KeyValuePair<string, object?>(HttpClientMetrics.HttpMethodLabel, method),
            new KeyValuePair<string, object?>(HttpClientMetrics.HttpTargetLabel, target), 
            new KeyValuePair<string, object?>(HttpClientMetrics.HttpResponseCodeLabel, 
                statusCode == null 
                    ? 0 
                    : (int)statusCode), new KeyValuePair<string, object?>(HttpClientMetrics.HttpSuccessLabel,
                success
                    ? HttpClientMetrics.HttpSuccessTrueValue
                    : HttpClientMetrics.HttpSuccessFalseValue));
    }
}
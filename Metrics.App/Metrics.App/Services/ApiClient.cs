namespace Metrics.App.Services;

class ApiClient: IApiClient
{
    public const string ApiClientName = "api-client";
    
    private readonly IHttpClientFactory _httpClientFactory;

    public ApiClient(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task GetData()
    {
        using var client = _httpClientFactory.CreateClient(ApiClientName);
        var request = new HttpRequestMessage(HttpMethod.Get, "https://64452e89b80f57f581b34e5e.mockapi.io/api/v1/data");
        var response = await client.SendAsync(request);
    }
}
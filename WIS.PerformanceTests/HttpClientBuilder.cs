namespace WIS.PerformanceTests;

public static class HttpClientBuilder
{
    public static HttpClient Create(Uri uri)
    {
        var handler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
        };
        
        var httpClient = new HttpClient(handler)
        {
            BaseAddress = uri
        };
        return httpClient;
    }
}
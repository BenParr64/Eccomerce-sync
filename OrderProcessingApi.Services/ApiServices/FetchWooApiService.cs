using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using OrderProcessingApi.Domain;
using OrderProcessingApi.Services.ApiServices.Interfaces;

namespace OrderProcessingApi.Services.ApiServices;

public class FetchWooApiService : FetchApiServiceBase, IFetchWooApiService
{
    private Integration _integration;

    public override HttpResponseMessage CallApi(string url)
    {
        var httpClient = new HttpClient();
        AddDefaultHeaders(httpClient);
        var response = httpClient.GetAsync(url);
        return response.Result;
    }

    public override HttpResponseMessage PostApi(string url, JsonContent jsonContent)
    {
        var requestMessage = new HttpRequestMessage(new HttpMethod("POST"), $"{url}");
        requestMessage.Content = jsonContent;
        var httpClient = new HttpClient();
        AddDefaultHeaders(httpClient);

        var response = httpClient.SendAsync(requestMessage).Result;
        return response;
    }

    public void SetCredentials(Integration integration)
    {
        _integration = integration;
    }

    private void AddDefaultHeaders(HttpClient httpClient)
    {
        var byteArray = Encoding.ASCII.GetBytes($"{_integration.WooConsumerKey}:{_integration.WooConsumerSecret}");
        httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
    }
}
namespace _10.StateManagement.Effects.Data.ChainingSync.Remote;

public sealed class OrderApiClient
{
    private readonly HttpClient _httpClient;

    public OrderApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<OrderDto> LoadOrderAsync(CancellationToken cancellationToken = default)
    {
        var result = await _httpClient.GetFromJsonAsync<OrderDto>("carts/1", cancellationToken);

        if (result is null)
        {
            throw new InvalidOperationException("Order response was empty.");
        }

        return result;
    }
}
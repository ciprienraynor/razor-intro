namespace _10.StateManagement.Effects.Data.Separation.Remote;

/// <summary>
/// ApiClient is the direct boundary to the outer world.
/// No business logic, no reducer logic, no UI concerns.
/// </summary>
public sealed class ProductApiClient
{
    private readonly HttpClient _httpClient;

    public ProductApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ProductDto> LoadProductAsync(int productId, CancellationToken cancellationToken = default)
    {
        var product = await _httpClient.GetFromJsonAsync<ProductDto>(
            $"products/{productId}",
            cancellationToken);

        if (product is null)
        {
            throw new InvalidOperationException("Product payload was empty.");
        }

        return product;
    }
}
namespace _10.StateManagement.Effects.Providers.ChainingAsync;


/// <summary>
/// Simple Provider just to show the second async step can go through
/// a Provider instead of another ApiClient.
/// </summary>
public sealed class CustomerProvider
{
    public async Task<string> LoadCustomerDisplayNameAsync(int userId, CancellationToken cancellationToken = default)
    {
        await Task.Delay(700, cancellationToken);
        return $"Customer #{userId}";
    }
}
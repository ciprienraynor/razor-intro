namespace _10.StateManagement.Effects.Providers.Shapes;

/// <summary>
/// Provider is useful here because this is runtime/application behavior,
/// not UI state ownership. No response is needed for the feature state.
/// </summary>
public sealed class AnalyticsProvider
{
    public Task TrackViewedAsync(string name, CancellationToken cancellationToken = default)
    {
        Console.WriteLine($"[Analytics] Viewed: {name}");
        return Task.CompletedTask;
    }
}
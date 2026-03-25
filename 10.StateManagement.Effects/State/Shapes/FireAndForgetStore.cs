using _10.StateManagement.Effects.Providers.Shapes;
using _10.StateManagement.Effects.State.Abstracts;

namespace _10.StateManagement.Effects.State.Shapes;

public sealed class FireAndForgetStore : StoreBase<FireAndForgetState, FireAndForgetAction>
{
    private readonly AnalyticsProvider _analyticsProvider;

    public FireAndForgetStore(AnalyticsProvider analyticsProvider)
        : base(FireAndForgetState.Default)
    {
        _analyticsProvider = analyticsProvider;
    }

    public async Task TrackViewAsync(CancellationToken cancellationToken = default)
    {
        Dispatch(new FireAndForgetAction.TrackViewRequested());

        // No result action comes back.
        await _analyticsProvider.TrackViewedAsync("Effects.Shapes.FireAndForget", cancellationToken);
    }

    protected override FireAndForgetState Reduce(FireAndForgetState state, FireAndForgetAction action)
    {
        return action switch
        {
            FireAndForgetAction.TrackViewRequested => state with
            {
                ClickCount = state.ClickCount + 1,
                LastIntent = "Tracking fired"
            },

            _ => state
        };
    }
}
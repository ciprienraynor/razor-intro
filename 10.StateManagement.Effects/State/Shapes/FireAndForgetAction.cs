namespace _10.StateManagement.Effects.State.Shapes;

public abstract record FireAndForgetAction
{
    public sealed record TrackViewRequested : FireAndForgetAction;
}
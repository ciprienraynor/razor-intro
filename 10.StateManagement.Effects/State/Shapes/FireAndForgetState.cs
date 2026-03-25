namespace _10.StateManagement.Effects.State.Shapes;

public sealed record FireAndForgetState(
    int ClickCount,
    string LastIntent)
{
    public static FireAndForgetState Default => new(
        ClickCount: 0,
        LastIntent: "Nothing tracked yet");
}
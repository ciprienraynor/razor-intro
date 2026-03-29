namespace _10.StateManagement.Effects.State.ChainingSync;

public sealed record ChainingSyncState(
    int Step,
    string Status)
{
    public static ChainingSyncState Default => new(
        Step: 0,
        Status: "Idle");
}
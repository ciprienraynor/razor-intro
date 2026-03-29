namespace _10.StateManagement.Effects.State.ChainingSync;

public abstract record ChainingSyncAction
{
    public sealed record Start : ChainingSyncAction;
    public sealed record Step1Completed : ChainingSyncAction;
    public sealed record Step2Completed : ChainingSyncAction;
}
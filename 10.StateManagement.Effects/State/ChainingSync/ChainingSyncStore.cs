using _10.StateManagement.Effects.State.Abstracts;

namespace _10.StateManagement.Effects.State.ChainingSync;

/// <summary>
/// Sync chaining means:
/// one action causes another action immediately, in sequence,
/// without async work in between.
/// </summary>
public sealed class ChainingSyncStore : StoreBase<ChainingSyncState, ChainingSyncAction>
{
    public ChainingSyncStore()
        : base(ChainingSyncState.Default)
    {
    }

    public void StartChain()
    {
        Dispatch(new ChainingSyncAction.Start());

        // Sync chaining: immediate follow-up actions
        Dispatch(new ChainingSyncAction.Step1Completed());
        Dispatch(new ChainingSyncAction.Step2Completed());
    }

    protected override ChainingSyncState Reduce(ChainingSyncState state, ChainingSyncAction action)
    {
        return action switch
        {
            ChainingSyncAction.Start => state with
            {
                Step = 0,
                Status = "Chain started"
            },

            ChainingSyncAction.Step1Completed => state with
            {
                Step = 1,
                Status = "Step 1 completed"
            },

            ChainingSyncAction.Step2Completed => state with
            {
                Step = 2,
                Status = "Step 2 completed"
            },

            _ => state
        };
    }
}
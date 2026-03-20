using _08.StateManagement.StoreGovernor.Components.Features.Counter.State;
using _08.StateManagement.StoreGovernor.Components.Shared.Components;
using Blazor.Diagnostics.Components;

namespace _08.StateManagement.StoreGovernor.Components.Features.Counter;

public partial class CounterDisplay : StoreComponent<CounterStore, CounterState>
{
    [Watch]
    public int Value => State.Count;
}
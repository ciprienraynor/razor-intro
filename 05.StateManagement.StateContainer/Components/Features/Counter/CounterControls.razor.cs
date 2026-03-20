using Microsoft.AspNetCore.Components;

namespace _05.StateManagement.StateContainer.Components.Features.Counter;

public partial class CounterControls : ComponentBase
{
    [Inject]
    private CounterStateContainer CounterState { get; set; } = null!;

    private void IncrementClicked()
    {
        CounterState.Increment();
    }

    private void ResetClicked()
    {
        CounterState.Reset();
    }
}
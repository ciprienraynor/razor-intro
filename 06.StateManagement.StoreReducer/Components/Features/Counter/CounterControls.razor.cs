using _06.StateManagement.StoreReducer.Components.Features.Counter.State;
using Microsoft.AspNetCore.Components;

namespace _06.StateManagement.StoreReducer.Components.Features.Counter;

public partial class CounterControls : ComponentBase
{
    [Inject]
    private CounterStore Store { get; set; } = null!;

    private void IncrementClicked()
    {
        Store.Dispatch(new IncrementAction());
    }

    private void ResetClicked()
    {
        Store.Dispatch(new ResetAction());
    }
}
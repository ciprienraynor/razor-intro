using Microsoft.AspNetCore.Components;

namespace _05.StateManagement.StateContainer.Components.Features.Counter;

public partial class CounterControls : ComponentBase
{
    [Parameter]
    public EventCallback OnIncrement { get; set; }

    [Parameter]
    public EventCallback OnReset { get; set; }

    private async Task IncrementClicked()
    {
        await OnIncrement.InvokeAsync();
    }

    private async Task ResetClicked()
    {
        await OnReset.InvokeAsync();
    }
}
using Microsoft.AspNetCore.Components;

namespace _07.StateManagement.Fluxor.Components.Features.Counter;

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
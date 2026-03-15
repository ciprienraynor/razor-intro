using Microsoft.AspNetCore.Components;

namespace _07.StateManagement.Fluxor.Components.Features.Counter;

public partial class CounterControlsSection : ComponentBase
{
    [Parameter]
    public EventCallback OnIncrement { get; set; }

    [Parameter]
    public EventCallback OnReset { get; set; }
}
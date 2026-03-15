using Microsoft.AspNetCore.Components;

namespace _05.StateManagement.StateContainer.Components.Features.Counter;

public partial class CounterControlsSection : ComponentBase
{
    [Parameter]
    public EventCallback OnIncrement { get; set; }

    [Parameter]
    public EventCallback OnReset { get; set; }
}
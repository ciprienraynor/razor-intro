using Microsoft.AspNetCore.Components;

namespace _06.StateManagement.StoreReducer.Components.Features.Counter;

public partial class CounterControlsSection : ComponentBase
{
    [Parameter]
    public EventCallback OnIncrement { get; set; }

    [Parameter]
    public EventCallback OnReset { get; set; }
}
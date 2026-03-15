using Microsoft.AspNetCore.Components;

namespace _04.Components.DeepNestingAndSiblings.Components.Features.Counter;

public partial class CounterControlsWrapper : ComponentBase
{
    [Parameter]
    public EventCallback OnIncrement { get; set; }

    [Parameter]
    public EventCallback OnReset { get; set; }
}
using Blazor.Diagnostics.Components;
using Microsoft.AspNetCore.Components;

namespace _04.Components.DeepNestingAndSiblings.Components.Features.Counter;

public partial class CounterDisplaySection : LoggingComponent
{
    [Parameter]
    [Watch]
    public int Value { get; set; }
}
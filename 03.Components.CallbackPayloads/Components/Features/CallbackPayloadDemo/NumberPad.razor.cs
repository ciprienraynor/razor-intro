using Blazor.Diagnostics.Components;
using Microsoft.AspNetCore.Components;

namespace _03.Components.CallbackPayloads.Components.Features.CallbackPayloadDemo;

public partial class NumberPad : LoggingComponent
{
    [Parameter]
    [Watch]
    public EventCallback<int> OnNumberSelected { get; set; }

    [Parameter]
    public EventCallback OnResetRequested { get; set; }

    private async Task Select(int number)
    {
        await OnNumberSelected.InvokeAsync(number);
    }

    private async Task Reset()
    {
        await OnResetRequested.InvokeAsync();
    }
}
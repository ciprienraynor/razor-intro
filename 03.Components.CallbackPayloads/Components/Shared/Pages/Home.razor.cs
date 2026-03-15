using Microsoft.AspNetCore.Components;

namespace _03.Components.CallbackPayloads.Components.Shared.Pages;

public partial class Home : ComponentBase
{
    private int selectedNumber;

    private void HandleNumberSelected(int number)
    {
        selectedNumber = number;
    }

    private void HandleReset()
    {
        selectedNumber = 0;
    }
}
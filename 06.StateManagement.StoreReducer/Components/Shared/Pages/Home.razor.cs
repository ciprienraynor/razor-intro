using Microsoft.AspNetCore.Components;

namespace _06.StateManagement.StoreReducer.Components.Shared.Pages;

public partial class Home : ComponentBase
{
    private int count = 0;

    private void IncrementCount()
    {
        count++;
    }

    private void ResetCount()
    {
        count = 0;
    }
}
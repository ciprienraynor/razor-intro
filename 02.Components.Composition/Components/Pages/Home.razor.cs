using Microsoft.AspNetCore.Components;

namespace _02.Components.Composition.Components.Pages;

public partial class Home : ComponentBase
{
    private int currentCount = 0;

    private void IncrementCount()
    {
        currentCount++;
    }

    private void ResetCount()
    {
        currentCount = 0;
    }
}
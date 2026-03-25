namespace _10.StateManagement.Effects.Services;

public sealed class FakeMessageService
{
    public async Task<string> LoadAsync()
    {
        await Task.Delay(1500);
        return "Hello from Effect";
    }
}
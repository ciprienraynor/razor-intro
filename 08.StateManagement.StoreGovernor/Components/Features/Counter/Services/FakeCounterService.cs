namespace _08.StateManagement.StoreGovernor.Components.Features.Counter.Services;

public class FakeCounterService : ICounterService
{
    private readonly Random _random = new();

    public async Task<int> GetRemoteCount()
    {
        await Task.Delay(1000);

        return _random.Next(0, 5) == 0 ? throw new Exception("Random failure") : _random.Next(1, 100);
    }
}
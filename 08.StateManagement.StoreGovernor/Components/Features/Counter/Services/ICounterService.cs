namespace _08.StateManagement.StoreGovernor.Components.Features.Counter.Services;

public interface ICounterService
{
    Task<int> GetRemoteCount();
}
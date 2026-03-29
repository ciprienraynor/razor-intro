namespace _11.StateManagement.SharedState.Core.BlazorKit;

public interface IWatchable
{
    IEnumerable<KeyValuePair<string, object?>> GetWatchedValues();
}
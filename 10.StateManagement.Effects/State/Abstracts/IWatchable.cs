namespace _10.StateManagement.Effects.State.Abstracts;

public interface IWatchable
{
    IEnumerable<KeyValuePair<string, object?>> GetWatchedValues();
}
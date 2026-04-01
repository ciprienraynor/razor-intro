# Using inside Store example

```csharp
private sealed record EffectType(string Name) : IEffectType
{
    public static readonly EffectType LoadProduct = new("LoadProduct");
    public static readonly EffectType SubmitOrder = new("SubmitOrder");
}

public async Task LoadProductAsync()
{
    await RunEffectAsync(
        effectType: EffectType.LoadProduct,
        mode: EffectStartMode.ReplaceExisting,
        async context =>
        {
            if (!context.Started)
                return;

            Dispatch(new MyAction.LoadStarted());

            try
            {
                var result = await _api.LoadAsync(context.Token);
                Dispatch(new MyAction.LoadCompleted(result));
            }
            catch (OperationCanceledException)
            {
                Dispatch(new MyAction.LoadCancelled());
            }
            catch (Exception ex)
            {
                Dispatch(new MyAction.LoadFailed(ex.Message));
            }
        });
}
```
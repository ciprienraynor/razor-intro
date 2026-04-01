namespace _10.StateManagement.Effects.State.Abstracts2;

/// <summary>
/// Base Store with:
/// - immutable state
/// - reducer-driven transitions
/// - effect lifecycle management
///
/// Important concepts:
/// - EffectType = logical slot/category
/// - RunningEffect = one concrete started run
/// - start mode applies at EffectType level
/// </summary>
public abstract class StoreBase2<TState, TAction> : IStore2<TState>
{
    /// <summary>
    /// Current immutable state.
    /// </summary>
    private TState _state;

    /// <summary>
    /// Protects all effect registry operations.
    /// </summary>
    private readonly object _effectsGate = new();

    /// <summary>
    /// Main registry:
    /// EffectType -> currently running instances of that type.
    ///
    /// One EffectType may have:
    /// - 0 instances
    /// - 1 instance
    /// - many instances (ParallelMultiInstance)
    /// </summary>
    private readonly Dictionary<IEffectType, List<RunningEffect>> _effectsByType = new();

    /// <summary>
    /// Current state exposed to Views.
    /// </summary>
    public TState State => _state;

    /// <summary>
    /// Raised whenever reducer produces a different state.
    /// </summary>
    public event Action? Changed;

    protected StoreBase2(TState initialState)
    {
        _state = initialState;
    }

    /// <summary>
    /// Main reducer entry point.
    /// </summary>
    public void Dispatch(TAction action)
    {
        var newState = Reduce(_state, action);

        if (EqualityComparer<TState>.Default.Equals(_state, newState))
            return;

        _state = newState;
        Changed?.Invoke();
    }

    /// <summary>
    /// Default Store behavior when View is disposed:
    /// cancel all running effects.
    /// </summary>
    public virtual void OnViewDisposed()
    {
        CancelAllEffects();
    }

    #region Effect Start / Registration

    /// <summary>
    /// Starts an effect under a given EffectType and start mode.
    ///
    /// The effect - executable logic that is always invoked.
    /// If effect was intentionally not started, context.Started = false.
    /// </summary>
    protected async Task RunEffectAsync(
        IEffectType effectType,
        EffectStartMode mode,
        Func<EffectContext, Task> effect)
    {
        RunningEffect? runningEffect;

        lock (_effectsGate)
        {
            runningEffect = TryStartEffect(effectType, mode);
        }

        if (runningEffect is null)
        {
            await effect(new EffectContext(
                Started: false,
                Token: CancellationToken.None,
                InstanceId: null));

            return;
        }

        try
        {
            await effect(new EffectContext(
                Started: true,
                Token: runningEffect.Cancellation.Token,
                InstanceId: runningEffect.InstanceId));
        }
        finally
        {
            lock (_effectsGate)
            {
                RemoveRunningEffect(runningEffect);
            }
        }
    }

    /// <summary>
    /// Applies start policy and either:
    /// - returns a newly started RunningEffect
    /// - or returns null when effect should not start
    /// </summary>
    private RunningEffect? TryStartEffect(
        IEffectType effectType,
        EffectStartMode mode)
    {
        var runningEffects = GetOrCreateRunningEffects(effectType);

        switch (mode)
        {
            case EffectStartMode.ReplaceExisting:
            {
                CancelAndRemoveAll(runningEffects);
                return CreateAndRegisterRunningEffect(effectType, runningEffects);
            }

            case EffectStartMode.IgnoreIfRunning:
            {
                if (runningEffects.Count > 0)
                    return null;

                return CreateAndRegisterRunningEffect(effectType, runningEffects);
            }

            case EffectStartMode.ParallelSingleInstance:
            {
                if (runningEffects.Count > 0)
                    return null;

                return CreateAndRegisterRunningEffect(effectType, runningEffects);
            }

            case EffectStartMode.ParallelMultiInstance:
            {
                return CreateAndRegisterRunningEffect(effectType, runningEffects);
            }

            default:
                throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
        }
    }

    /// <summary>
    /// Returns the list of running instances for this EffectType.
    /// Creates it if missing.
    /// </summary>
    private List<RunningEffect> GetOrCreateRunningEffects(IEffectType effectType)
    {
        if (!_effectsByType.TryGetValue(effectType, out var runningEffects))
        {
            runningEffects = new List<RunningEffect>();
            _effectsByType[effectType] = runningEffects;
        }

        return runningEffects;
    }

    /// <summary>
    /// Creates one concrete RunningEffect and adds it to the registry.
    /// </summary>
    private static RunningEffect CreateAndRegisterRunningEffect(
        IEffectType effectType,
        List<RunningEffect> runningEffects)
    {
        var runningEffect = new RunningEffect(
            effectType: effectType,
            instanceId: Guid.NewGuid(),
            cancellation: new CancellationTokenSource());

        runningEffects.Add(runningEffect);
        return runningEffect;
    }

    #endregion

    #region Effect Cancellation

    /// <summary>
    /// Cancels all currently running instances of one EffectType.
    /// </summary>
    protected void CancelEffect(IEffectType effectType)
    {
        lock (_effectsGate)
        {
            if (_effectsByType.TryGetValue(effectType, out var runningEffects))
            {
                CancelAndRemoveAll(runningEffects);
            }
        }
    }

    /// <summary>
    /// Cancels all running effects of all types.
    /// </summary>
    protected void CancelAllEffects()
    {
        lock (_effectsGate)
        {
            foreach (var runningEffects in _effectsByType.Values.ToList())
            {
                CancelAndRemoveAll(runningEffects);
            }

            _effectsByType.Clear();
        }
    }

    /// <summary>
    /// Cancels and removes all RunningEffects from the given list.
    /// </summary>
    private void CancelAndRemoveAll(List<RunningEffect> runningEffects)
    {
        foreach (var runningEffect in runningEffects.ToList())
        {
            runningEffect.Cancellation.Cancel();
            runningEffect.Cancellation.Dispose();
            runningEffects.Remove(runningEffect);
        }
    }

    #endregion

    #region Effect Cleanup

    /// <summary>
    /// Removes one finished RunningEffect from the registry.
    ///
    /// This is normal completion cleanup.
    /// It does NOT cancel anything.
    /// </summary>
    private void RemoveRunningEffect(RunningEffect runningEffect)
    {
        if (!_effectsByType.TryGetValue(runningEffect.EffectType, out var runningEffects))
            return;

        runningEffects.RemoveAll(x => x.InstanceId == runningEffect.InstanceId);

        runningEffect.Cancellation.Dispose();

        if (runningEffects.Count == 0)
        {
            _effectsByType.Remove(runningEffect.EffectType);
        }
    }

    #endregion

    #region Reducer Contract

    /// <summary>
    /// Reducer must remain pure.
    /// </summary>
    protected abstract TState Reduce(TState state, TAction action);

    #endregion

    #region Internal Runtime Model

    /// <summary>
    /// One concrete started run of an EffectType.
    ///
    /// EffectType:
    /// - logical slot/category
    ///
    /// InstanceId:
    /// - unique id of this concrete run
    ///
    /// Cancellation:
    /// - CTS controlling this concrete run
    /// </summary>
    private sealed class RunningEffect
    {
        public IEffectType EffectType { get; }
        public Guid InstanceId { get; }
        public CancellationTokenSource Cancellation { get; }

        public RunningEffect(
            IEffectType effectType,
            Guid instanceId,
            CancellationTokenSource cancellation)
        {
            EffectType = effectType;
            InstanceId = instanceId;
            Cancellation = cancellation;
        }
    }

    #endregion
}
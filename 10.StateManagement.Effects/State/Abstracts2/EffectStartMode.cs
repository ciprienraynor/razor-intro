namespace _10.StateManagement.Effects.State.Abstracts2;

/// <summary>
/// Defines what to do when starting a new effect of the same EffectType.
/// </summary>
public enum EffectStartMode
{
    /// <summary>
    /// Cancel existing running instances of the same EffectType,
    /// then start a new one.
    /// </summary>
    ReplaceExisting,

    /// <summary>
    /// If any instance of the same EffectType is already running,
    /// do not start a new one.
    /// </summary>
    IgnoreIfRunning,

    /// <summary>
    /// Different EffectTypes may run at the same time,
    /// but the same EffectType may still only have one live instance.
    /// </summary>
    ParallelSingleInstance,

    /// <summary>
    /// Allow multiple simultaneous instances of the same EffectType.
    /// Use intentionally and rarely.
    /// </summary>
    ParallelMultiInstance
}
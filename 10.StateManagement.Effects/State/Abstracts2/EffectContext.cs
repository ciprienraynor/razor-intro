namespace _10.StateManagement.Effects.State.Abstracts2;

/// <summary>
/// Runtime context passed into an effect body.
///
/// Started:
/// - true  => this effect really started
/// - false => Store intentionally skipped this run
///
/// Token:
/// - valid cancellation token when Started = true
/// - CancellationToken.None when Started = false
///
/// InstanceId:
/// - unique id of this concrete running effect
/// - null when Started = false
/// </summary>
public readonly record struct EffectContext(
    bool Started,
    CancellationToken Token,
    Guid? InstanceId);
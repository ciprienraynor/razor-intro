using _11.StateManagement.SharedState.Core.Models;
using _11.StateManagement.SharedState.Core.State;

namespace _11.StateManagement.SharedState.Core.Providers;

/// <summary>
/// Shared application concern.
/// Owns ShoppingCartState and timer lifecycle.
/// </summary>
public sealed class ShoppingCartProvider : IDisposable
{
    private readonly object _gate = new();
    private ShoppingCartState _state = ShoppingCartState.Default;
    private CancellationTokenSource? _timerCts;

    public event Action? Changed;

    public ShoppingCartState CurrentState
    {
        get
        {
            lock (_gate)
            {
                return _state;
            }
        }
    }

    public void AddItem(string id, string name, decimal unitPrice)
    {
        var shouldStartTimer = false;

        lock (_gate)
        {
            var existing = _state.Cart.Items.FirstOrDefault(item => item.Id == id);

            IReadOnlyList<ShoppingCartItem> nextItems;

            if (existing is null)
            {
                nextItems = _state.Cart.Items
                    .Append(new ShoppingCartItem(id, name, 1, unitPrice))
                    .ToArray();
            }
            else
            {
                nextItems = _state.Cart.Items
                    .Select(item => item.Id == id
                        ? item with { Quantity = item.Quantity + 1 }
                        : item)
                    .ToArray();
            }

            var nextCart = _state.Cart with { Items = nextItems };

            _state = _state with
            {
                Cart = nextCart,
                IsExpired = false
            };

            if (!_state.IsTimerRunning)
            {
                shouldStartTimer = true;
            }
        }

        RaiseChanged();

        if (shouldStartTimer)
        {
            StartCountdown(60);
        }
    }

    public void RemoveItem(string id)
    {
        lock (_gate)
        {
            var existing = _state.Cart.Items.FirstOrDefault(item => item.Id == id);
            if (existing is null)
            {
                return;
            }

            IReadOnlyList<ShoppingCartItem> nextItems =
                existing.Quantity > 1
                    ? _state.Cart.Items.Select(item => item.Id == id
                        ? item with { Quantity = item.Quantity - 1 }
                        : item).ToArray()
                    : _state.Cart.Items.Where(item => item.Id != id).ToArray();

            var nextCart = _state.Cart with { Items = nextItems };

            if (nextCart.IsEmpty)
            {
                StopCountdownInternal();

                _state = _state with
                {
                    Cart = nextCart,
                    RemainingSeconds = 0,
                    IsTimerRunning = false,
                    IsExpired = false
                };
            }
            else
            {
                _state = _state with
                {
                    Cart = nextCart
                };
            }
        }

        RaiseChanged();
    }

    public void ClearCart()
    {
        lock (_gate)
        {
            StopCountdownInternal();

            _state = _state with
            {
                Cart = ShoppingCart.Empty,
                RemainingSeconds = 0,
                IsTimerRunning = false,
                IsExpired = false
            };
        }

        RaiseChanged();
    }

    public void StartCountdown(int seconds)
    {
        lock (_gate)
        {
            StopCountdownInternal();

            _state = _state with
            {
                RemainingSeconds = seconds,
                IsTimerRunning = true,
                IsExpired = false
            };

            _timerCts = new CancellationTokenSource();
        }

        RaiseChanged();

        _ = RunTimerAsync(_timerCts.Token);
    }

    private async Task RunTimerAsync(CancellationToken cancellationToken)
    {
        try
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(1000, cancellationToken);

                var shouldExpire = false;

                lock (_gate)
                {
                    if (!_state.IsTimerRunning)
                    {
                        return;
                    }

                    var nextSeconds = Math.Max(0, _state.RemainingSeconds - 1);

                    _state = _state with
                    {
                        RemainingSeconds = nextSeconds
                    };

                    if (nextSeconds == 0)
                    {
                        shouldExpire = true;
                    }
                }

                RaiseChanged();

                if (shouldExpire)
                {
                    ExpireCart();
                    return;
                }
            }
        }
        catch (OperationCanceledException)
        {
            // expected
        }
    }

    private void ExpireCart()
    {
        lock (_gate)
        {
            StopCountdownInternal();

            _state = _state with
            {
                Cart = ShoppingCart.Empty,
                RemainingSeconds = 0,
                IsTimerRunning = false,
                IsExpired = true
            };
        }

        RaiseChanged();
    }

    private void StopCountdownInternal()
    {
        _timerCts?.Cancel();
        _timerCts?.Dispose();
        _timerCts = null;
    }

    private void RaiseChanged()
    {
        Changed?.Invoke();
    }

    public void Dispose()
    {
        lock (_gate)
        {
            StopCountdownInternal();
        }
    }
}
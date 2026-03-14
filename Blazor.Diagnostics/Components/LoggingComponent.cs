using System.Reflection;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

namespace Blazor.Diagnostics.Components;


public abstract class LoggingComponent : ComponentBase
{
    [Inject]
    protected ILoggerFactory LoggerFactory { get; set; } = default!;

    protected override void OnAfterRender(bool firstRender)
    {
        var logger = LoggerFactory.CreateLogger(GetType().Name);

        var watchedProps = GetType()
            .GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
            .Where(p => p.GetCustomAttribute<WatchAttribute>() is not null)
            .Select(p => $"{p.Name}: {p.GetValue(this)}")
            .ToArray();

        var state = watchedProps.Length > 0
            ? string.Join(" | ", watchedProps)
            : "No watched properties";

        logger.LogInformation(
            "🔄 [{Mode}] {State}",
            firstRender ? "INIT" : "PUSH",
            state);
    }
}
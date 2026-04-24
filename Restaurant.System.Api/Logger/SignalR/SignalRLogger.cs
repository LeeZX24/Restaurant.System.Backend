using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.SignalR;

public class SignalRLogger : ILogger
{
    private readonly IHubContext<LogHub> _hub;

    public SignalRLogger(IHubContext<LogHub> hub)
    {
        _hub = hub;
    }

    public IDisposable BeginScope<TState>(TState state) => null!;

    public bool IsEnabled(LogLevel logLevel) => true;

    public void Log<TState>(
        LogLevel logLevel,
        EventId eventId,
        TState state,
        Exception? exception,
        Func<TState, Exception?, string> formatter)
    {
        var message = $"[{DateTime.Now:HH:mm:ss}] [{logLevel}] {formatter(state, exception)}";

        _hub.Clients.All.SendAsync("ReceiveLog", message);
    }
}
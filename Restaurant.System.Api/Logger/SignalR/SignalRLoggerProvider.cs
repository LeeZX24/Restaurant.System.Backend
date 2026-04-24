using Microsoft.AspNetCore.SignalR;

public class SignalRLoggerProvider : ILoggerProvider
{
    private readonly IHubContext<LogHub> _hub;

    public SignalRLoggerProvider(IHubContext<LogHub> hub)
    {
        _hub = hub;
    }

    public ILogger CreateLogger(string categoryName)
    {
        return new SignalRLogger(_hub);
    }

    public void Dispose() { }
}
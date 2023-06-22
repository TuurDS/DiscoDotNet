using DiscoDotNet.Services.Intefaces;

namespace DiscoDotNet.Services;

public class DiscordConnectClient : IDiscordConnectClient
{
    private readonly ILogger<DiscordConnectClient> _logger;

    private string Channel { get; set; } = string.Empty;

    public DiscordConnectClient(ILogger<DiscordConnectClient> logger)
    {
        _logger = logger;
    }

    public Task ConnectAsync(string channel)
    {
        _logger.LogInformation("Connecting to {Channel}", channel);
        Channel = channel;
        return Task.CompletedTask;
    }

    public Task DisconnectAsync()
    {
        _logger.LogInformation("Disconnecting from {Channel}", Channel);
        Channel = string.Empty;
        return Task.CompletedTask;
    }
}

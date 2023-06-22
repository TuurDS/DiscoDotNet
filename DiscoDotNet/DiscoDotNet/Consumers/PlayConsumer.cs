using DiscoDotNet.Services.Intefaces;

namespace DiscoDotNet.Consumers;

public record PlayRequest
{
    public string Song { get; set; } = default!;
}

public class PlayConsumer : IConsumer<PlayRequest>
{
    
    private readonly ILogger<PlayConsumer> _logger;
    private readonly IDiscordConnectClient _discordConnectClient;

    public PlayConsumer(ILogger<PlayConsumer> logger, IDiscordConnectClient discordConnectClient)
    {
        _logger = logger;
        _discordConnectClient = discordConnectClient;
    }

    public async Task Consume(ConsumeContext<PlayRequest> context)
    {
        await _discordConnectClient.ConnectAsync("#main");
        _logger.LogInformation("PlayConsumer: {Song}", context.Message.Song);
        await Task.Delay(5000);
        await _discordConnectClient.DisconnectAsync();
    }
}
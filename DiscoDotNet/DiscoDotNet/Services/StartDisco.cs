using DiscoDotNet.Consumers;
using MassTransit.Mediator;
using Microsoft.Extensions.Hosting;

namespace DiscoDotNet.Services;

public class StartDisco : BackgroundService
{
    private readonly ILogger<StartDisco> _logger;
    private readonly IMediator _mediator;

    public StartDisco(ILogger<StartDisco> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var song = "Never Gonna Give You Up";
        _logger.LogInformation("Sending {Song} to PlayConsumer", song);
        
        var message = new PlayRequest
        {
            Song = song
        };
        
        await _mediator.Send(message, stoppingToken);
    }
}

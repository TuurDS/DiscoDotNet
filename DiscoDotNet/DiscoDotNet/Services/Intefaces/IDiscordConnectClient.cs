
namespace DiscoDotNet.Services.Intefaces;

public interface IDiscordConnectClient
{
    public Task ConnectAsync(string channel);
    public Task DisconnectAsync();
}

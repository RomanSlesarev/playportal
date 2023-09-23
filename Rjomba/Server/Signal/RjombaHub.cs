using Microsoft.AspNetCore.SignalR;
using Rjomba.Server.Services;

namespace BlazorWebAssemblySignalRApp.Server.Hubs;

public class RjombaHub : Hub
{
    private RjombaGamesService _rjombaService;

    public RjombaHub(RjombaGamesService rjombaService)
    {
        _rjombaService = rjombaService;
    }

    public async Task InitializeGame(string GameId)
    {
        _rjombaService.Add(GameId);
    }

    public async Task SendMessage(string user, string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }
}
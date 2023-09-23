using System.Collections.Concurrent;

namespace Rjomba.Server.Services
{
    public class RjombaGamesService : BackgroundService
    {
        private ConcurrentDictionary<string, DateTime> _games;

        public RjombaGamesService()
        {
            _games = new ConcurrentDictionary<string, DateTime>();
        }

        public void Add(string gameId)
        {
            _games.TryAdd(gameId, DateTime.Now);
        }

        public void Update(string gameId)
        {
            _games[gameId] = DateTime.Now;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(5000);

                foreach (var game in _games)
                {
                    if (DateTime.Now - game.Value > TimeSpan.FromMinutes(5))
                    {
                        _games.TryRemove(game);
                    }
                }
            }
        }
    }
}
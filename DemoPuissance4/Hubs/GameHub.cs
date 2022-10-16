using DemoPuissance4.Entity;
using DemoPuissance4.Services;
using Microsoft.AspNetCore.SignalR;

namespace DemoPuissance4.Hubs
{
    public class GameHub : Hub
    {
        private static List<Game> _games = new List<Game>();

        private readonly GameService _gameService;

        public GameHub(GameService gameService)
        {
            _gameService = gameService;
        }

        // Créer la partie
        public void Create()
        {
            // Récupérer access_token dans la requête
            string player = Context.GetHttpContext()!.Request.Query["access_token"];
            Game game = _gameService.Create(player);
            _games.Add(game);
            // Envoyer l'info à tous les clients
            Clients.All.SendAsync("allTables", _games);
        }

        // Joindre une partie
        public void Join()
        {

        }

        // Fait partie du cycle de vie
        // A la connexion
        public override Task OnConnectedAsync()
        {
            // Caller = celui qui vient de se connecter
            Clients.Caller.SendAsync("allTables", _games);
            return base.OnConnectedAsync();
        }

        // Fait partie du cycle de vie
        // A la déconnexion
        public override Task OnDisconnectedAsync(Exception? exception)
        {
            // Récupérer access_token dans la requête
            string player = Context.GetHttpContext()!.Request.Query["access_token"];
            Game? game = _games.FirstOrDefault(game => game.Player1 == player || game.Player2 == player);
            if (game != null)
            {
                // si un player quitte la partie est supprimée
                _games.Remove(game);
            }

            // todo: envoyer une notif au groupe
            Clients.All.SendAsync("allTables", _games);

            return base.OnDisconnectedAsync(exception);
        }

    }
}

using DemoPuissance4.Entity;

namespace DemoPuissance4.Services
{
    public class GameService
    {
        public Game Create(string player)
        {
            Game game = new Game();
            game.Id = Guid.NewGuid();
            int alea = new Random().Next(0, 2);
            if (alea == 1)
            {
                game.Player1 = player;
            }
            else
            {
                game.Player2 = player;
            }

            return game;
        }
    }
}

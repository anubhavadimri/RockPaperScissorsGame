using RockPaperScissorsGame.Model.Requests;
using RockPaperScissorsGame.Service;

namespace RockPaperScissorsGame
{
    class Program
    {
        static void Main(string[] args)
        {
            string gameName = "Game1";
            string firstPlayerName = "Player1";
            string secondPlayerName = "Player2";

            GameService gameService = new Service.GameService();
            CreateGameRequest createGameRequest = new CreateGameRequest
            {
                GameName = gameName
            };
            var createResponse = gameService.CreateGame(createGameRequest);

            JoinGameRequest firstPlayerRequest = new JoinGameRequest
            {
                GameName = gameName,
                Player = new Model.Player
                {
                    Id = new System.Guid(),
                    Name = firstPlayerName
                }
            };

            gameService.JoinGame(firstPlayerRequest);

            JoinGameRequest secondPlayerRequest = new JoinGameRequest
            {
                GameName = gameName,
                Player = new Model.Player
                {
                    Id = new System.Guid(),
                    Name = secondPlayerName
                }
            };

            gameService.JoinGame(secondPlayerRequest);

            PlayGameRequest playGameRequest = new PlayGameRequest
            {
                  GameName = gameName,
                  PlayerName = firstPlayerName
            };
            var gameResponse = gameService.StartGame(playGameRequest);
            if (gameResponse.IsSuccessful)
            {
                
            }
        }
    }
}

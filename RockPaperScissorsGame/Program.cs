using RockPaperScissorsGame.Model.Requests;
using RockPaperScissorsGame.Service;
using System;

namespace RockPaperScissorsGame
{
    class Program
    {
        static void Main(string[] args)
        {
            string gameName = string.Empty;
            string firstPlayerName = string.Empty;
            string secondPlayerName = string.Empty;

            Console.Clear();
            Console.WriteLine("Please Enter Game Name : ");
            gameName = Console.ReadLine();
            Console.WriteLine("Please Enter First Player Name : ");
            firstPlayerName = Console.ReadLine();
            Console.WriteLine("Please Enter Second Player Name : ");
            secondPlayerName = Console.ReadLine();

            GameService gameService = new GameService();
            CreateGameRequest createGameRequest = new CreateGameRequest
            {
                GameName = gameName
            };
            var createResponse = gameService.CreateGame(createGameRequest);
            if (createResponse.IsSuccessful)
                Console.WriteLine("Game successfully created.");
            JoinGameRequest firstPlayerRequest = new JoinGameRequest
            {
                GameName = gameName,
                Player = new Model.Player
                {
                    Id = new Guid(),
                    Name = firstPlayerName
                }
            };

            var joiner1response = gameService.JoinGame(firstPlayerRequest);
            if (joiner1response.IsSuccessful)
                Console.WriteLine(firstPlayerName + " joined game.");
            JoinGameRequest secondPlayerRequest = new JoinGameRequest
            {
                GameName = gameName,
                Player = new Model.Player
                {
                    Id = new Guid(),
                    Name = secondPlayerName
                }
            };

            var joiner2response = gameService.JoinGame(secondPlayerRequest);
            if (joiner2response.IsSuccessful)
                Console.WriteLine(secondPlayerName + " joined game.");

            Console.WriteLine("Hi " + firstPlayerName + " ----- Please choose any option from 1 to 3 ----");
            int input = Convert.ToInt32(Console.ReadLine());


            PlayGameRequest playGameRequest = new PlayGameRequest
            {
                GameName = gameName,
                PlayerName = firstPlayerName,
                NextMove = (Enums.Move)input
            };
            Console.WriteLine(firstPlayerName + " Move.");
            var gameResponse = gameService.StartGame(playGameRequest);

            Console.WriteLine("Hi " + secondPlayerName + " ----- Please choose any option from 1 to 3 ----");
            int secondInput = Convert.ToInt32(Console.ReadLine());
            PlayGameRequest secondGameRequest = new PlayGameRequest
            {
                GameName = gameName,
                PlayerName = secondPlayerName,
                NextMove = (Enums.Move)secondInput
            };

            Console.WriteLine(secondPlayerName + " Move.");
            var secondGameResponse = gameService.StartGame(secondGameRequest);

            //Generating Result
            if (gameResponse.IsSuccessful && secondGameResponse.IsSuccessful)
            {
                Console.WriteLine("Game Completed. Generating Result ");
                GameStatusRequest gameStatusRequest = new GameStatusRequest
                { GameId = createResponse.GameId };
                var result = gameService.CheckGameStatus(gameStatusRequest);
                if (result.IsSuccessful)
                {

                    if (result.Status == Enums.GameStatus.PlayerOneWon)
                    {
                        Console.WriteLine("Result Status . " + firstPlayerName + " Won");
                    }
                    if (result.Status == Enums.GameStatus.PlayerTwoWon)
                    {
                        Console.WriteLine("Result Status . " + secondPlayerName + " Won");
                    }
                    else
                    {
                        Console.WriteLine("Result Status . " + result.Status);
                    }
                }
            }
            else
            {
                Console.WriteLine("Invalid Entry.");
            }
            Console.WriteLine("Enter any key to exit.");
            Console.WriteLine("--------------------------------------------");
        }
    }
}

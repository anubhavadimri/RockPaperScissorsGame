using RockPaperScissorsGame.Model.Requests;
using RockPaperScissorsGame.Service;
using System;

namespace RockPaperScissorsGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Rock, Paper, Scissors Game!");
            Console.WriteLine("You are playing this game with Computer.");
            Console.WriteLine("PLayer , what is your name? : ");
            var player1Name = Console.ReadLine();
            var player2Name = "Computer";

            GameService gameService = new GameService();
            var gameProfile = gameService.CreateGameProfile(); //Create Game Profile

            // Registration of 1nd player in Team
            JoinTeam player1 = new JoinTeam
            {
                Player = new Model.Player
                {
                    Id = new Guid(),
                    Name = player1Name
                }
            };
            gameService.JoinTeam(player1);

            // Registration of 2nd player in Team
            JoinTeam player2 = new JoinTeam
            {
                Player = new Model.Player
                {
                    Id = new Guid(),
                    Name = player2Name
                }
            };
            gameService.JoinTeam(player2);


            Console.WriteLine("Welcome {0} and {1}", player1Name, player2Name);
            Console.WriteLine("{0}, Please pick Rock, Paper, or Scissors. Press 1 for Rock, 2 for Paper, 3 for Scissors", player1Name);
            int input = Convert.ToInt32(Console.ReadLine());

            PlayGameRequest playGameRequest = new PlayGameRequest
            {
                PlayerName = player1Name,
                NextMove = (Enums.MoveType)input
            };
            var gameResponse = gameService.StartMove(playGameRequest);

            Random random = new Random();            
            int secondInput = random.Next(1, 3);
            PlayGameRequest secondGameRequest = new PlayGameRequest
            {
                PlayerName = player2Name,
                NextMove = (Enums.MoveType)secondInput
            };
            var secondGameResponse = gameService.StartMove(secondGameRequest);

            //Moves done and calculate Result
            if (gameResponse.IsSuccessful && secondGameResponse.IsSuccessful)
            {
                GameStatusRequest gameStatusRequest = new GameStatusRequest
                { GameId = gameProfile.GameId };
                var result = gameService.CheckGameStatus(gameStatusRequest);
                if (result.IsSuccessful)
                {
                    Console.WriteLine("==================================");
                    if (result.Status == Enums.GameStatus.PlayerOneWon)
                        Console.WriteLine("Player : " + player1Name + " is the winner");
                    else if (result.Status == Enums.GameStatus.PlayerTwoWon)
                        Console.WriteLine("Player : " + player2Name + " is the winner");
                    else
                        Console.WriteLine("Player : " + result.Status);
                    
                }
            }
            Console.WriteLine("Press any key to exit.");
        }
    }
}

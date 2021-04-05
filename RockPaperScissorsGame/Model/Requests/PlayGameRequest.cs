using RockPaperScissorsGame.Enums;

namespace RockPaperScissorsGame.Model.Requests
{
    public class PlayGameRequest
    {
        public MoveType NextMove { get; set; }

        public string PlayerName { get; set; }
    }
}

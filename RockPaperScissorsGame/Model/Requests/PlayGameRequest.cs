using RockPaperScissorsGame.Enums;

namespace RockPaperScissorsGame.Model.Requests
{
    public class PlayGameRequest
    {
        public Move NextMove { get; set; }

        public string GameName { get; set; }

        public string PlayerName { get; set; }
    }
}

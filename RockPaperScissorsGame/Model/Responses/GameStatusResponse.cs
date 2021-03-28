using RockPaperScissorsGame.Enums;

namespace RockPaperScissorsGame.Model.Responses
{
    public class GameStatusResponse
    {
        public bool IsSuccessful { get; set; }
        public GameStatus Status { get; set; }
        public ResponseError Error { get; set; }
    }
}

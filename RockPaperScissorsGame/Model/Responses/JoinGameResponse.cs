namespace RockPaperScissorsGame.Model.Responses
{
    public class JoinGameResponse
    {
        public bool IsSuccessful { get; set; }
        public ResponseError Error { get; set; }
    }
}

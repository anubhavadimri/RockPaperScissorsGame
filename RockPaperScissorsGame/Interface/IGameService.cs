using RockPaperScissorsGame.Model.Requests;
using RockPaperScissorsGame.Model.Responses;

namespace RockPaperScissorsGame.Interface
{
    public interface IGameService
    {
        CreateGameResponse CreateGame(CreateGameRequest request);

        JoinGameResponse JoinGame(JoinGameRequest request);

        PlayGameResponse StartGame(PlayGameRequest request);

        GameStatusResponse CheckGameStatus(GameStatusRequest request);
    }
}

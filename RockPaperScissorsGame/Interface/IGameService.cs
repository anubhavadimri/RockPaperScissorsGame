using RockPaperScissorsGame.Model.Requests;
using RockPaperScissorsGame.Model.Responses;

namespace RockPaperScissorsGame.Interface
{
    public interface IGameService
    {
        CreateGameResponse CreateGameProfile();

        JoinGameResponse JoinTeam(JoinTeam request);

        PlayGameResponse StartMove(PlayGameRequest request);

        GameStatusResponse CheckGameStatus(GameStatusRequest request);
    }
}

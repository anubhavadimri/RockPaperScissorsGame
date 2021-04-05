using RockPaperScissorsGame.Enums;
using RockPaperScissorsGame.Model;
using RockPaperScissorsGame.Model.Requests;
using System;
using Xunit;

namespace RockPaperScissorsGame.Tests
{
    public class RequestTests
    {
        private readonly Player testPlayer;
        private readonly Guid playerGuid;
        private readonly Guid gameGuid;
        private readonly string playerName;
        private readonly MoveType nextMove;

        public RequestTests()
        {
            this.nextMove = MoveType.Empty;
            this.playerName = "Test Player";
            this.playerGuid = Guid.NewGuid();
            this.gameGuid = Guid.NewGuid();
            this.testPlayer = new Player
            {
                Id = this.playerGuid,
                Move = MoveType.Empty,
                Name = "Test Player One"
            };
        }

        [Fact]
        public void PlayGameRequest_Correct_ObjectCreated()
        {
            // Arrange
            var playGameRequest = new PlayGameRequest
            {
                PlayerName = this.playerName,
                NextMove = this.nextMove
            };

            // Act
            // Assert
            Assert.Equal(this.playerName, playGameRequest.PlayerName);
            Assert.Equal(this.nextMove, playGameRequest.NextMove);
        }

        [Fact]
        public void JoinGameRequest_Correct_ObjectCreated()
        {
            // Arrange
            var joinGameRequest = new JoinTeam()
            {
                Player = this.testPlayer
            };

            // Act
            // Assert
            Assert.Equal(this.testPlayer, joinGameRequest.Player);
        }

        [Fact]
        public void GameStatusRequest_Correct_ObjectCreated()
        {
            // Arrange
            var gameStatusRequest = new GameStatusRequest
            {
                GameId = this.gameGuid
            };

            // Act
            // Assert
            Assert.Equal(this.gameGuid, gameStatusRequest.GameId);
        }
    }
}

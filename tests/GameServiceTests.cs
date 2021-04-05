using RockPaperScissorsGame.Enums;
using RockPaperScissorsGame.Interface;
using RockPaperScissorsGame.Model.Requests;
using RockPaperScissorsGame.Service;
using System;
using System.Net;
using Xunit;

namespace RockPaperScissorsGame.Tests
{
    public class GameServiceTests
    {
        private readonly IGameService gameService;

        public GameServiceTests()
        {
            this.gameService = new GameService(); 
        }
         
        [Fact]
        public void GetGameStatus_EmptyId_ReturnBadRequest()
        {
            // Arrange
            var gameStatusRequest = new GameStatusRequest
            {
                GameId = Guid.Empty
            };

            // Act
            var gameStatusResponse = this.gameService.CheckGameStatus(gameStatusRequest);

            // Assert
            Assert.False(gameStatusResponse.IsSuccessful);
            Assert.Equal(HttpStatusCode.BadRequest, gameStatusResponse.Error.ErrorCode);
        }

        [Fact]
        public void GetGameStatus_InvalidId_ReturnBadRequest()
        {
            // Arrange
            
            var createGameResponse = this.gameService.CreateGameProfile();
            var gameStatusRequest = new GameStatusRequest
            {
                GameId = createGameResponse.GameId
            };

            // Act
            var gameStatusResponse = this.gameService.CheckGameStatus(gameStatusRequest);

            // Assert
            Assert.True(gameStatusResponse.IsSuccessful);
            Assert.Equal(GameStatus.Created, gameStatusResponse.Status);
        }

        [Fact]
        public void GetGameStatus_CorrectId_ReturnOk()
        {
            // Arrange
            var gameStatusRequest = new GameStatusRequest
            {
                GameId = Guid.NewGuid()
            };

            // Act
            var gameStatusResponse = this.gameService.CheckGameStatus(gameStatusRequest);

            // Assert
            Assert.False(gameStatusResponse.IsSuccessful);
            Assert.Equal(HttpStatusCode.BadRequest, gameStatusResponse.Error.ErrorCode);
        }
    }
}

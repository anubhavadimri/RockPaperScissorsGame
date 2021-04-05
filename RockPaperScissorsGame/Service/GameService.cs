using RockPaperScissorsGame.Enums;
using RockPaperScissorsGame.Interface;
using RockPaperScissorsGame.Model;
using RockPaperScissorsGame.Model.Requests;
using RockPaperScissorsGame.Model.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace RockPaperScissorsGame.Service
{
    public class GameService : IGameService
    {
        private readonly List<Game> games;

        public GameService()
        {
            this.games = new List<Game>();
        }

        /// <summary>
        /// Create a new game
        /// </summary>        
        /// <returns>Create Game Response</returns>
        public CreateGameResponse CreateGameProfile()
        {
            // Just create now
            var game = new Game
            {
                Id = Guid.NewGuid(),
                Status = GameStatus.Created
            };
            this.games.Add(game);

            return new CreateGameResponse { GameId = game.Id };
        }

        public JoinGameResponse JoinTeam(JoinTeam request)
        {
            // Validate request, you will never know again
            if (string.IsNullOrEmpty(request?.Player?.Name))
            {
                return new JoinGameResponse
                {
                    IsSuccessful = false,
                    Error = new ResponseError
                    {
                        ErrorCode = HttpStatusCode.BadRequest,
                        Description = HttpStatusCode.BadRequest.ToString()
                    }
                };
            }

            // Validate Game exists
            var game = this.games.FirstOrDefault();
            if (game == null)
            {
                return new JoinGameResponse
                {
                    IsSuccessful = false,
                    Error = new ResponseError
                    {
                        ErrorCode = HttpStatusCode.BadRequest,
                        Description = HttpStatusCode.BadRequest.ToString()
                    }
                };
            }

            // Validate Game isn't full or finished
            if (game.IsFull || game.IsFinished)
            {
                return new JoinGameResponse
                {
                    IsSuccessful = false,
                    Error = new ResponseError
                    {
                        ErrorCode = HttpStatusCode.BadRequest,
                        Description = HttpStatusCode.BadRequest.ToString()
                    }
                };
            }

            // The real joining happens now
            // First player will just join
            if (game.FirstPlayer == null)
            {
                game.FirstPlayer = request.Player;
            }

            // Second player is joining
            else if (game.SecondPlayer == null)
            {
                game.SecondPlayer = request.Player;

                // Set flag for the future
                game.IsFull = true;
            }
            else
            {
                return new JoinGameResponse
                {
                    IsSuccessful = false,
                    Error = new ResponseError
                    {
                        ErrorCode = HttpStatusCode.BadRequest,
                        Description = HttpStatusCode.BadRequest.ToString()
                    }
                };
            }

            return new JoinGameResponse
            {
                IsSuccessful = true
            };
        }

        public PlayGameResponse StartMove(PlayGameRequest request)
        {
            // Validate request, you will never know again and again
            if (string.IsNullOrEmpty(request?.PlayerName) || request?.NextMove == MoveType.Empty)
            {
                return new PlayGameResponse
                {
                    IsSuccessful = false,
                    Error = new ResponseError
                    {
                        ErrorCode = HttpStatusCode.BadRequest,
                        Description = HttpStatusCode.BadRequest.ToString()
                    }
                };
            }

            // Validate Game exists
            var game = this.games.FirstOrDefault();
            if (game == null)
            {
                return new PlayGameResponse
                {
                    IsSuccessful = false,
                    Error = new ResponseError
                    {
                        ErrorCode = HttpStatusCode.BadRequest,
                        Description = HttpStatusCode.BadRequest.ToString()
                    }
                };
            }

            // Validate Game isn't finished
            if (game.IsFinished)
            {
                return new PlayGameResponse
                {
                    IsSuccessful = false,
                    Error = new ResponseError
                    {
                        ErrorCode = HttpStatusCode.BadRequest,
                        Description = HttpStatusCode.BadRequest.ToString()
                    }
                };
            }

            // Validate that player is allowed to play in this game
            if (game.FirstPlayer.Name != request.PlayerName && game.SecondPlayer.Name != request.PlayerName)
            {
                return new PlayGameResponse
                {
                    IsSuccessful = false,
                    Error = new ResponseError
                    {
                        ErrorCode = HttpStatusCode.BadRequest,
                        Description = HttpStatusCode.BadRequest.ToString()
                    }
                };
            }

            // Time to play
            // If player one move is pending
            if (game.FirstPlayer.Name == request.PlayerName)
            {
                switch (game.Status)
                {
                    case GameStatus.PlayerOneMovePending:
                        game.FirstPlayer.Move = request.NextMove;
                        game.IsFinished = true;
                        break;
                    case GameStatus.Created:
                        game.FirstPlayer.Move = request.NextMove;
                        game.Status = GameStatus.PlayerTwoMovePending;
                        break;
                    default:
                        return new PlayGameResponse
                        {
                            IsSuccessful = false,
                            Error = new ResponseError
                            {
                                ErrorCode = HttpStatusCode.BadRequest,
                                Description = HttpStatusCode.BadRequest.ToString()
                            }
                        };
                }
            }

            // If player two move is pending
            else if (game.SecondPlayer.Name == request.PlayerName)
            {
                switch (game.Status)
                {
                    case GameStatus.PlayerTwoMovePending:
                        game.SecondPlayer.Move = request.NextMove;
                        game.IsFinished = true;
                        break;
                    case GameStatus.Created:
                        game.SecondPlayer.Move = request.NextMove;
                        game.Status = GameStatus.PlayerOneMovePending;
                        break;
                    default:
                        return new PlayGameResponse
                        {
                            IsSuccessful = false,
                            Error = new ResponseError
                            {
                                ErrorCode = HttpStatusCode.BadRequest,
                                Description = HttpStatusCode.BadRequest.ToString()
                            }
                        };
                }
            }

            return new PlayGameResponse
            {
                IsSuccessful = true
            };
        }

        public GameStatusResponse CheckGameStatus(GameStatusRequest request)
        {
            // Validate request, you will never know again, again and again
            if (request.GameId == Guid.Empty)
            {
                return new GameStatusResponse
                {
                    IsSuccessful = false,
                    Error = new ResponseError
                    {
                        ErrorCode = HttpStatusCode.BadRequest,
                        Description = HttpStatusCode.BadRequest.ToString()
                    }
                };
            }

            // Validate Game exists
            var game = this.games.FirstOrDefault(item => item.Id == request.GameId); 
            return new GameStatusResponse
            {
                IsSuccessful = true,
                Status = game.IsFinished ? CheckResult(game.FirstPlayer.Move, game.SecondPlayer.Move) : game.Status
            };
        }

        private static GameStatus CheckResult(MoveType firstPlayerMove, MoveType secondPlayerMove)
        {
            switch (firstPlayerMove)
            {
                case MoveType.Paper when secondPlayerMove == MoveType.Rock:
                    return GameStatus.PlayerOneWon;
                case MoveType.Paper when secondPlayerMove == MoveType.Scissors:
                    return GameStatus.PlayerTwoWon;
                case MoveType.Scissors when secondPlayerMove == MoveType.Paper:
                    return GameStatus.PlayerOneWon;
                case MoveType.Scissors when secondPlayerMove == MoveType.Rock:
                    return GameStatus.PlayerTwoWon;
                case MoveType.Rock when secondPlayerMove == MoveType.Paper:
                    return GameStatus.PlayerTwoWon;
                case MoveType.Rock when secondPlayerMove == MoveType.Scissors:
                    return GameStatus.PlayerOneWon;
                default:
                    return GameStatus.Tie;
            }
        }

    }
}

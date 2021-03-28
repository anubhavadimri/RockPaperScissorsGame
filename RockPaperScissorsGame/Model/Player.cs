using RockPaperScissorsGame.Enums;
using System;

namespace RockPaperScissorsGame.Model
{
    public class Player
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Move Move { get; set; }
    }
}

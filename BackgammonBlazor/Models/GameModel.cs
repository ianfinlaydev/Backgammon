using BackgammonBlazor.Helpers;
using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace BackgammonBlazor.Models
{
    public class GameModel
    {
        #region Public Properties
        //TODO: Change a lot of these properties to private fields with method access
        public Dictionary<int, BoardPointModel> Points { get; set; } = [];

        public PlayerModel[] Players { get; set; } = new PlayerModel[2];

        public PlayerModel ActivePlayer { get; set; } = new();

        public List<TurnModel> Turns { get; set; } = [];

        public TurnModel ActiveTurn { get; set; } = new();
        #endregion Public Properties

        #region Public Methods
        public PlayerModel GetPlayer(PlayerColor color)
            => Players.First(p => p.PlayerColor == color);

        public void ChangeActivePlayer()
            => ActivePlayer = Players.First(p => p.PlayerColor != ActivePlayer.PlayerColor);

        public void ChangeActivePlayer(PlayerColor playerColor)
            => ActivePlayer = Players.First(p => p.PlayerColor == playerColor);

        public BoardPointModel GetPoint(int pointNumber)
            => Points[pointNumber];

        public BoardPointModel GetPoint(CheckerColor checkerColor)
            => GetPoint((int)checkerColor);

        public void CompleteActiveTurn()
        {
            if (!ActiveTurn.IsComplete())
            {
                throw new ArgumentException("Turn is invalid", nameof(ActiveTurn));
            }

            Turns.Add(ActiveTurn);
        }

        public bool MakeMove(int origin)
        {
            foreach (var value in ActivePlayer.Dice.Values)
            {
                int destination = ActivePlayer.GetDestinationPointNumber(origin, value);

                //TODO: Adjust this validation when player is able to bear off
                //Destination is off the board
                if (destination < 1 || destination > 24)
                {
                    continue;
                }

                var move = new MoveModel
                {
                    Origin = GetPoint(origin),
                    Destination = GetPoint(destination)
                };

                if (ActivePlayer.CanMakeMove(move))
                {
                    ActivePlayer.Dice.Values.Remove(value);
                    ActiveTurn.AddMove(move);

                    if (move.IsHit())
                    {
                        var checker = move.GetHitChecker();
                        var bar = GetPoint(checker.CheckerColor);
                        checker.Move(move.Destination, bar);
                    }

                    move.Origin?.Checkers.FirstOrDefault()?.Move(move.Origin, move.Destination);
                    return true;
                }
            }

            return false;
        }

        public void UndoMove()
        {
            //TODO: This doesn't work
            var move = ActiveTurn.UndoMove();
            UpdatePoints(move);
        }

        public void UpdatePoints(MoveModel move)
        {
            if (move.IsHit())
            {
                var checker = move.GetHitChecker();
                var bar = GetPoint(checker.CheckerColor);
                checker.Move(move.Destination, bar);
            }

            move.Origin.Checkers.FirstOrDefault()?.Move(move.Origin, move.Destination);
        }

        public bool IsComplete() 
            => Players.Any(p => p.PipCount == 0);
        #endregion Public Methods
    }
}

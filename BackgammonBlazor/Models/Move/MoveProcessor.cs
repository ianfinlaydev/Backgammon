using BackgammonBlazor.Models.Point;
using BackgammonBlazor.Models.Checker;
using BackgammonBlazor.Models.Game;
using System.Xml.Linq;

namespace BackgammonBlazor.Models.Move
{
    public class MoveProcessor(GameModel game)
    {
        private GameModel _game = game;

        public void ProcessMove(MoveModel move)
        {
            if (move.IsHit())
            {
                PointModel bar = _game.GetBarPoint(move.Destination.Checkers.First().PlayerColor);
                move.SetHittingMove(move.Destination, bar);
                move.HittingMove.Process();
            }

            CheckerModel checkerToMove = move.Origin.Checkers.First();
            checkerToMove.Point = move.Destination;
            move.Origin.Checkers.Remove(checkerToMove);
            move.Destination.Checkers.Add(checkerToMove);
        }

        public void UndoMove(MoveModel move)
        {
            if (move.IsHit())
            {
            }

        }

    }
}

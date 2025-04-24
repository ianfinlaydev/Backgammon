using BackgammonBlazor.Models.Point;
using BackgammonBlazor.Models.Game;
using BackgammonBlazor.Models.Player;

namespace BackgammonBlazor.Models.Checker
{
    public class CheckerModel(GameModel gameModel, PointModel point, PlayerColor playerColor)
    {
        #region Public Properties
        public GameModel GameModel { get; set; } = gameModel;

        public PointModel Point { get; set; } = point;

        public PlayerColor PlayerColor { get; set; } = playerColor;
        #endregion Public Properties

        public void MoveChecker(PointModel origin, PointModel destination)
        {
            Point = destination;
            origin.Checkers.Remove(this);
            destination.Checkers.Add(this);
        }
    }
}

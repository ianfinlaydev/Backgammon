using BackgammonBlazor.Models.BoardPoint;
using BackgammonBlazor.Models.Game;
using BackgammonBlazor.Models.Player;

namespace BackgammonBlazor.Models.Checker
{
    public class CheckerModel(GameModel gameModel, BoardPointModel point, PlayerColor playerColor)
    {
        #region Public Properties
        public GameModel GameModel { get; set; } = gameModel;

        public BoardPointModel Point { get; set; } = point;

        public PlayerColor PlayerColor { get; set; } = playerColor;
        #endregion Public Properties

        public void MoveChecker(BoardPointModel origin, BoardPointModel destination)
        {
            Point = destination;
            origin.Checkers.Remove(this);
            destination.Checkers.Add(this);
        }
    }
}

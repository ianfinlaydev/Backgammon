namespace BackgammonBlazor.Models
{
    public class CheckerModel(GameModel gameModel, BoardPointModel point, CheckerColor checkerColor)
    {
        #region Public Properties
        public GameModel GameModel { get; set; } = gameModel;

        public BoardPointModel Point { get; set; } = point;

        public CheckerColor CheckerColor { get; set; } = checkerColor;
        #endregion Public Properties

        public void MoveChecker(BoardPointModel origin, BoardPointModel destination)
        {
            Point = destination;
            origin.Checkers.Remove(this);
            destination.Checkers.Add(this);
        }
    }

    public enum CheckerColor
    {
        Light = 25,
        Dark = 0,
    }
}

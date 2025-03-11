using BackgammonBlazor.Models.BoardPoint;
using BackgammonBlazor.Models.Checker;
using BackgammonBlazor.Models.Game;

namespace BackgammonBlazor.Models.Player
{
    public class PlayerModel(GameModel gameModel, PlayerColor playerColor)
    {
        public GameModel GameModel { get; set; } = gameModel;

        public int PipCount { get; set; } = _initialPipCount;

        public PlayerColor PlayerColor { get; set; } = playerColor;

        public bool IsActivePlayer { get; set; } = false;

        private const int _initialPipCount = 167;

        #region Public Methods
        public bool HasCheckersOnPoint(BoardPointModel point)
            => point.HasCheckersOfPlayer(this);

        public bool HasCheckersOnBar()
            => GameModel
            .GetBarPoint(PlayerColor)
            .HasCheckersOfPlayer(this);

        public bool HasAllCheckersInHomeBoard()
            => GetCheckers().All(c =>
            c.PlayerColor == PlayerColor.Light ?
            c.Point.PointNumber <= 6 :
            c.Point.PointNumber >= 19);

        internal bool CanMoveCheckerOnHigherPointNumber(int pointNumber, int consumedDiceValue)
        {
            return GetCheckers().Select(c => c.Point.PointNumber)
                .Any(pn =>
                PlayerColor == PlayerColor.Light ?
                pn > pointNumber :
                pn < pointNumber);
        }

        private IEnumerable<CheckerModel> GetCheckers()
            => GameModel.Checkers
            .Where(c => c.PlayerColor == PlayerColor);

        public void SetPipCount()
            => PipCount = GameModel.Checkers
            .Where(c => c.PlayerColor == PlayerColor)
            .Select(c => c.PlayerColor == PlayerColor.Light ?
                c.Point.PointNumber :
                25 - c.Point.PointNumber)
            .Sum();
        #endregion Public Methods
    }

    public enum PlayerColor
    {
        Light = 25,
        Dark = 0,
    }
}


using BackgammonBlazor.Helpers;

namespace BackgammonBlazor.Models
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

        //TODO: Below method is incomplete.
        public bool HasCheckersOnBar()
            => GameModel.GetPoint(PlayerColor);

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

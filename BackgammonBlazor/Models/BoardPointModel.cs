using BackgammonBlazor.Helpers;

namespace BackgammonBlazor.Models
{
    public class BoardPointModel(GameModel gameModel, int pointNumber)
    {
        #region Public Properties
        public GameModel GameModel { get; set; } = gameModel;

        public int PointNumber { get; set; } = pointNumber;

        public List<CheckerModel> Checkers { get; set; } = [];
        #endregion Public Properties

        #region Public Methods
        public bool HasCheckers()
            => Checkers.Count > 0;

        public bool HasCheckersOfPlayer(PlayerModel player)
            => HasCheckers() && player.Matches(Checkers.First());

        public bool IsHittable()
            => Checkers.Count == 1;

        public bool IsMade()
            => Checkers.Count > 1;

        public bool IsMadeByPlayer(PlayerModel player)
            => IsMade() && HasCheckersOfPlayer(player);
        #endregion Public Methods

        public bool IsTopPoint()
            => PointNumber > 12;

        public bool IsBottomPoint()
            => !IsTopPoint();

        public bool IsLightPoint()
            => PointNumber % 2 == 0;

        public bool IsDarkPoint()
            => !IsLightPoint();
    }
}

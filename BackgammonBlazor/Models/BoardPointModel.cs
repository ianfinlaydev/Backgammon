using BackgammonBlazor.Helpers;
using System.ComponentModel.DataAnnotations;

namespace BackgammonBlazor.Models
{
    public class BoardPointModel
    {
        #region Public Properties
        public int PointNumber { get; set; }

        public List<CheckerModel> Checkers { get; set; } = [];

        #endregion Public Properties

        #region Public Methods
        public bool HasCheckers()
            => Checkers.Count > 0;

        public bool HasCheckersOfPlayer(PlayerModel player)
            => HasCheckers() && player.PlayerColor.EqualsEnum(Checkers.First().CheckerColor);

        public bool IsHittable()
            => Checkers.Count == 1;

        public bool IsMade()
            => Checkers.Count > 1;

        public bool IsMadeByVillain(PlayerModel hero)
            => IsMade() && !HasCheckersOfPlayer(hero);
        #endregion Public Methods

        //TODO: Move to css nth element?
        public bool IsTopPoint()
            => PointNumber > 12;

        //TODO: Move to css nth element?
        public bool IsLightPoint()
            => PointNumber % 2 == 0;

        //TODO: Move to css nth element?
        public bool IsDarkPoint()
            => !IsLightPoint();
    }
}

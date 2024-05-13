using BackgammonBlazor.Helpers;

namespace BackgammonBlazor.Models
{
    public class MoveModel
    {
        #region Public Properties
        public BoardPointModel Origin { get; set; } = new();
        public BoardPointModel Destination { get; set; } = new();
        #endregion Public Properties

        #region Public Methods
        public bool IsHit()
        {
            //Destination does not have a checker to hit
            if (!Destination.IsHittable())
            {
                return false;
            }

            //The checkers on both points belong to the same player
            if (Origin.Checkers.First().CheckerColor == Destination.Checkers.First().CheckerColor)
            {
                return false;
            }

            return true;
        }

        public CheckerModel GetChecker()
            => Origin.Checkers.First();

        public CheckerModel GetHitChecker()
            => Destination.Checkers.First();
        #endregion Public Methods
    }
}

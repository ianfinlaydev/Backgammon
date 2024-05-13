using BackgammonBlazor.Helpers;

namespace BackgammonBlazor.Models
{
    public class PlayerModel
    {
        private const int InitialPipCount = 167;

        public DiceModel Dice { get; set; } = new();

        public PlayerColor PlayerColor { get; set; }

        public int PipCount { get; set; } = InitialPipCount;

        #region Public Methods
        public bool CanMakeMove(MoveModel move)
        {
            //No checkers to move
            if (!move.Origin.HasCheckers())
            {
                return false;
            }

            //Point is made by opposing player
            if (move.Destination.IsMadeByVillain(this))
            {
                return false;
            }

            return true;
        }
        public void RollDice()
            => Dice.Roll();

        public bool HasCheckersOnPoint(BoardPointModel point)
            => point.HasCheckers() && point.Checkers.First().CheckerColor.EqualsEnum(PlayerColor);

        public int GetDestinationPointNumber(int origin, int value)
            => PlayerColor == PlayerColor.Light ? origin - value : origin + value;
        #endregion Public Methods
    }

    public enum PlayerColor
    {
        Light = int.MinValue,
        Dark = int.MaxValue,
    }
}

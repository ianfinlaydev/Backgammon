using BackgammonBlazor.Models.Point;
using BackgammonBlazor.Models.Checker;
using BackgammonBlazor.Models.Game;
using BackgammonBlazor.Models.Player;

namespace BackgammonBlazor.Models.Move
{
    public class MoveModel
    {
        #region Public Properties
        public PointModel Origin { get; private set; }

        public PointModel Destination { get; private set; }

        public int ConsumedDiceValue { get; private set; }

        public MoveModel HittingMove { get; private set; }
        #endregion Public Properties

        #region Private Fields
        private readonly GameModel _game;
        private readonly MoveValidator _validator;
        private readonly MoveProcessor _processor;
        #endregion Private Fields

        #region Constructor
        public MoveModel(
            GameModel game,
            int consumedDiceValue,
            PointModel origin,
            PointModel destination = null)
        {
            _game = game;
            _validator = new MoveValidator(_game);
            _processor = new MoveProcessor(_game);

            Origin = origin;
            Destination = destination ?? CalculateDestinationPoint(origin.PointNumber, consumedDiceValue);
            ConsumedDiceValue = consumedDiceValue;
        }
        #endregion Constructor

        #region Public Methods
        public bool IsValid() => _validator.IsValidMove(this);

        public void Process() => _processor.ProcessMove(this);

        public void Undo() => _processor.UndoMove(Reverse());

        public void SetHittingMove(PointModel origin, PointModel bar)
        {
            HittingMove = new(_game, -1, origin, bar);
        }

        public MoveModel Reverse()
        {
            if (IsHit())
            {
                (Origin, Destination, HittingMove.Origin, HittingMove.Destination) = (HittingMove.Destination, HittingMove.Origin, Destination, Origin);
            }
            else
            {
                (Origin, Destination) = (Destination, Origin);
            }

            return this;
        }


        public bool IsHit()
        {
            //This move is composed with a hitting move
            if (HittingMove != null)
            {
                return true;
            }

            //Destination is hittable and has villains checker
            if (Destination.IsHittable() && Origin.Checkers.First().PlayerColor != Destination.Checkers.First().PlayerColor)
            {
                return true;
            }

            return false;
        }
        #endregion Public Methods

        #region Private Methods
        private PointModel CalculateDestinationPoint(int origin, int consumedDiceValue)
        {
            PlayerColor playerColor = _game.Hero.PlayerColor;

            int destination = playerColor == PlayerColor.Light ? origin - consumedDiceValue : origin + consumedDiceValue;

            //If destination is borne off point
            if (destination < 1 || destination > 24)
            {
                return _game.GetBorneOffPoint(playerColor);
            }

            return _game.GetPoint(destination);
        }
        #endregion Private Methods
    }
}

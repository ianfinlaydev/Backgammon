namespace BackgammonBlazor.Models
{
    public class MoveModel(GameModel gameModel, BoardPointModel origin, BoardPointModel destination, int consumedDiceValue = 0)
    {
        #region Public Properties
        //TODO: Explore making these properties private with public method access and editing
        public GameModel GameModel { get; private set; } = gameModel;

        public BoardPointModel Origin { get; private set; } = origin;

        public BoardPointModel Destination { get; private set; } = destination;

        public int ConsumedDiceValue { get; set; } = consumedDiceValue;
        #endregion Public Properties

        private MoveModel _hittingMove;

        public MoveModel(GameModel gameModel, BoardPointModel origin, int value) : this(gameModel, origin, null, value)
        {
            Destination = GetDestinationPoint(origin.PointNumber, value);
        }

        private BoardPointModel GetDestinationPoint(int origin, int value)
            => GameModel.GetPoint(
                GameModel.ActivePlayer.PlayerColor == PlayerColor.Light ? 
                origin - value : 
                origin + value);

        #region Public Methods
        public void Process()
        {
            if (IsHit())
            {
                _hittingMove ??= new MoveModel(GameModel, Destination, GetBarOfHitChecker());

                _hittingMove.Process();
            }

            CheckerModel checkerToMove = GetCheckerToMove();
            checkerToMove.Point = Destination;
            Origin.Checkers.Remove(checkerToMove);
            Destination.Checkers.Add(checkerToMove);
        }

        public bool IsHit()
        {
            //This move is composed with a hitting move
            if (_hittingMove != null)
            {
                return true;
            }

            //Destination has a checker to hit from the opposing player
            if (Destination.IsHittable() && GetCheckerToMove().CheckerColor != GetHitChecker().CheckerColor)
            {
                return true;
            }

            return false;
        }

        public MoveModel Reverse()
        {
            if (IsHit())
            {
                (Origin, Destination, _hittingMove.Origin, _hittingMove.Destination) = (_hittingMove.Destination, _hittingMove.Origin, Destination, Origin);
            }
            else
            {
                (Origin, Destination) = (Destination, Origin);
            }

            return this;
        }

        public CheckerModel GetCheckerToMove()
            => Origin.Checkers.First();

        public CheckerModel GetHitChecker()
            => Destination.Checkers.First();

        public BoardPointModel GetBarOfHitChecker()
            => GameModel.GetPoint(GetHitChecker().CheckerColor);
        #endregion Public Methods
    }
}

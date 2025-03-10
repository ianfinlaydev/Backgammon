
namespace BackgammonBlazor.Models
{
    public class GameModel
    {
        #region Public Properties
        //TODO: Change a lot of these properties to private fields with method access
        public List<PlayerModel> Players { get; set; } = [];

        //TODO: Change to Hero and Villain
        public PlayerModel Hero { get; set; }

        public PlayerModel Villain { get; set; }

        public Dictionary<int, BoardPointModel> Points { get; set; } = [];

        public List<CheckerModel> Checkers { get; set; } = [];

        public DiceModel Dice { get; set; }

        public List<TurnModel> Turns { get; set; } = [];

        public TurnModel ActiveTurn { get; set; }
        #endregion Public Properties

        #region Public Methods
        public PlayerModel GetPlayer(PlayerColor color)
            => Players.First(p => p.PlayerColor == color);

        public void ChangeActivePlayer()
        {
            foreach (var player in Players)
            {
                player.IsActivePlayer = !player.IsActivePlayer;
            }

            Hero = Players.First(p => p.IsActivePlayer);
            Villain = Players.First(p => !p.IsActivePlayer);
        }

        public BoardPointModel GetPoint(int pointNumber)
            => Points[pointNumber];

        public BoardPointModel GetPoint(PlayerColor playerColor)
            => GetPoint((int)playerColor);

        public void StartNewTurn()
        {
            ActiveTurn = new(this);
        }

        public void CompleteTurn()
        {
            if (!IsCompleteTurn())
            {
                return;
            }

            Turns.Add(ActiveTurn);
            ChangeActivePlayer();
        }

        public bool TryMove(BoardPointModel origin)
        {
            if (!Hero.HasCheckersOnPoint(origin))
            {
                return false;
            }

            foreach (var diceValue in Dice.Values)
            {
                MoveModel move = new(this, origin, diceValue);

                if (IsValidMove(move))
                {
                    ProcessMove(move);
                    return true;
                }
            }

            return false;
        }

        private bool IsValidMove(MoveModel move)
        {
            //TODO: Add bearing off logic

            //Hero needs to bear on checkers from their bar
            if (Hero.HasCheckersOnBar() && move.Origin != GetPoint(Hero.PlayerColor))
            {
                return false;
            }

            //Point is made by villain
            if (move.Destination.IsMadeByPlayer(Villain))
            {
                return false;
            }

            return true;
        }

        public void ProcessMove(MoveModel move)
        {
            move.Process();

            ActiveTurn.AddMove(move);

            UpdatePipCounts();

            Dice.UseDiceValue(move.ConsumedDiceValue);
        }

        public void UndoMove()
        {
            //TODO: Error when undoing moves where the undo dice order matters (made points blocking)
            MoveModel moveToUndo = ActiveTurn.GetLastMove();

            moveToUndo.Reverse().Process();

            ActiveTurn.RemoveLastMove();

            UpdatePipCounts();

            Dice.UnuseDiceValue(moveToUndo.ConsumedDiceValue);
        }

        private void UpdatePipCounts()
        {
            foreach (var player in Players)
            {
                player.SetPipCount();
            }
        }

        public bool IsCompleteTurn()
            => Dice.Values.Count == 0;

        public bool IsCompleteGame() 
            => Players.Any(p => p.PipCount == 0);
        #endregion Public Methods
    }
}

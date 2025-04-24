using BackgammonBlazor.Models.Point;
using BackgammonBlazor.Models.Checker;
using BackgammonBlazor.Models.Dice;
using BackgammonBlazor.Models.Move;
using BackgammonBlazor.Models.Player;
using BackgammonBlazor.Models.Turn;

namespace BackgammonBlazor.Models.Game
{
    public class GameModel(MoveFactory moveFactory)
    {
        #region Public Properties
        public List<PlayerModel> Players { get; set; } = [];

        public PlayerModel Hero { get; set; }

        public PlayerModel Villain { get; set; }

        public Dictionary<int, PointModel> Points { get; set; } = [];

        public List<CheckerModel> Checkers { get; set; } = [];

        public DiceModel Dice { get; set; }

        public List<TurnModel> Turns { get; set; } = [];

        public TurnModel ActiveTurn { get; set; }
        #endregion Public Properties

        private MoveFactory _moveFactory = moveFactory;

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

        public PointModel GetPoint(int pointNumber)
            => Points[pointNumber];

        public PointModel GetBarPoint(PlayerColor playerColor)
            => GetPoint((int)playerColor);

        public PointModel GetBorneOffPoint(PlayerColor playerColor)
            => GetPoint(playerColor == PlayerColor.Light ? (int)BorneOffPoint.Light : (int)BorneOffPoint.Dark);

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

        public bool TryMove(PointModel origin)
        {
            if (!Hero.HasCheckersOnPoint(origin))
            {
                return false;
            }

            foreach (var diceValue in Dice.GetUnusedValues())
            {
                MoveModel move = _moveFactory.CreateMove(origin, diceValue);

                if (move.IsValid())
                {
                    move.Process();

                    ActiveTurn.AddMove(move);

                    UpdatePipCounts();

                    Dice.UseDiceValue(move.ConsumedDiceValue);

                    return true;
                }
            }

            return false;
        }

        public void UndoMove()
        {
            //TODO: Error when undoing moves where the undo dice order matters (made points blocking)
            MoveModel moveToUndo = ActiveTurn.GetLastMove();

            moveToUndo.Undo();

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
            => !Dice.HasUnusedValues();

        public bool IsCompleteGame()
            => Players.Any(p => p.PipCount == 0);
        #endregion Public Methods
    }
}

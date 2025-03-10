using BackgammonBlazor.Models;
using System.Drawing;

namespace BackgammonBlazor.Helpers
{
    public class GameInitializer()
    {
        private GameModel _gameModel;
        private static readonly Random _random = new();

        public void InitializeGame(GameModel gameModel)
        {
            _gameModel = gameModel;

            InitializeDice();
            InitializePlayers();
            InitializeFirstTurn();
            InitializePoints();
            InitializeCheckers();
        }

        private void InitializeDice() 
            => _gameModel.Dice = new DiceModel(_gameModel);

        private void InitializePlayers()
        {
            _gameModel.Players.Add(new PlayerModel(_gameModel, PlayerColor.Light));

            _gameModel.Players.Add(new PlayerModel(_gameModel, PlayerColor.Dark));

            GetRandomPlayer().IsActivePlayer = true;

            _gameModel.ActivePlayer = _gameModel.Players.First(p => p.IsActivePlayer);
        }

        private PlayerModel GetRandomPlayer()
            => _gameModel.Players[_random.Next(0, 2)];

        private void InitializeFirstTurn()
            => _gameModel.StartNewTurn();

        private void InitializePoints()
        {
            for (int i = 0; i <= 25; i++)
            {
                _gameModel.Points.Add(i, new BoardPointModel(_gameModel, i));
            }
        }

        private void InitializeCheckers()
        {
            foreach (var (pointNumber, numCheckers, checkerColor) in InitialCheckerInfo)
            {
                for (int i = 0; i < numCheckers; i++)
                {
                    BoardPointModel point = _gameModel.GetPoint(pointNumber);

                    CheckerModel checker = new(_gameModel, point, checkerColor);

                    point.Checkers.Add(checker);
                }
            }
        }

        private readonly (int pointNumber, int numCheckers, CheckerColor checkerColor)[] InitialCheckerInfo =
        [
            ( 1, 2, CheckerColor.Dark ),
            ( 6, 5, CheckerColor.Light ),
            ( 8, 3, CheckerColor.Light ),
            ( 12, 5, CheckerColor.Dark ),
            ( 13, 5, CheckerColor.Light ),
            ( 17, 3, CheckerColor.Dark ),
            ( 19, 5, CheckerColor.Dark ),
            ( 24, 2, CheckerColor.Light ),
        ];
    }
}

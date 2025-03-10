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

            _gameModel.Hero = _gameModel.Players.First(p => p.IsActivePlayer);
            _gameModel.Villain = _gameModel.Players.First(p => !p.IsActivePlayer);
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
            foreach (var (pointNumber, numCheckers, playerColor) in InitialCheckerInfo)
            {
                for (int i = 0; i < numCheckers; i++)
                {
                    BoardPointModel point = _gameModel.GetPoint(pointNumber);

                    CheckerModel checker = new(_gameModel, point, playerColor);

                    point.Checkers.Add(checker);
                }
            }
        }

        private readonly (int pointNumber, int numCheckers, PlayerColor checkerColor)[] InitialCheckerInfo =
        [
            ( 1, 2, PlayerColor.Dark ),
            ( 6, 5, PlayerColor.Light ),
            ( 8, 3, PlayerColor.Light ),
            ( 12, 5, PlayerColor.Dark ),
            ( 13, 5, PlayerColor.Light ),
            ( 17, 3, PlayerColor.Dark ),
            ( 19, 5, PlayerColor.Dark ),
            ( 24, 2, PlayerColor.Light ),
        ];
    }
}

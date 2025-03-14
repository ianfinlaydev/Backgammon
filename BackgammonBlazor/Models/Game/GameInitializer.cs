using BackgammonBlazor.Models.BoardPoint;
using BackgammonBlazor.Models.Checker;
using BackgammonBlazor.Models.Dice;
using BackgammonBlazor.Models.Player;

namespace BackgammonBlazor.Models.Game
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
            for (int i = 1; i <= 24; i++)
            {
                _gameModel.Points.Add(i, new BoardPointModel(_gameModel, i));
            }

            //Borne Off Points
            _gameModel.Points.Add((int)BorneOffPoint.Light, new BoardPointModel(_gameModel, (int)BorneOffPoint.Light)); //PlayerColor.Light BorneOff
            _gameModel.Points.Add((int)BorneOffPoint.Dark, new BoardPointModel(_gameModel, (int)BorneOffPoint.Dark)); //PlayerColor.Dark BorneOff

            //Bar Points
            _gameModel.Points.Add((int)PlayerColor.Light, new BoardPointModel(_gameModel, (int)PlayerColor.Light)); //PlayerColor.Light
            _gameModel.Points.Add((int)PlayerColor.Dark, new BoardPointModel(_gameModel, (int)PlayerColor.Dark)); //PlayerColor.Dark

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

                    _gameModel.Checkers.Add(checker);
                }
            }
        }

        private readonly (int pointNumber, int numCheckers, PlayerColor checkerColor)[] InitialCheckerInfo =
        [
            //( 1, 2, PlayerColor.Dark ),
            //( 6, 5, PlayerColor.Light ),
            //( 8, 3, PlayerColor.Light ),
            //( 12, 5, PlayerColor.Dark ),
            //( 13, 5, PlayerColor.Light ),
            //( 17, 3, PlayerColor.Dark ),
            //( 19, 5, PlayerColor.Dark ),
            //( 24, 2, PlayerColor.Light ),
            
            ( 18, 2, PlayerColor.Dark ),
            ( 19, 2, PlayerColor.Dark ),
            ( 20, 2, PlayerColor.Dark ),
            ( 21, 2, PlayerColor.Dark ),
            ( 22, 2, PlayerColor.Dark ),
            ( 23, 2, PlayerColor.Dark ),
            ( 7, 2, PlayerColor.Light ),
            ( 6, 2, PlayerColor.Light ),
            ( 5, 2, PlayerColor.Light ),
            ( 4, 2, PlayerColor.Light ),
            ( 3, 2, PlayerColor.Light ),
            ( 2, 2, PlayerColor.Light ),
        ];
    }
}

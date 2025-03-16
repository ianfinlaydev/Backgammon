using BackgammonBlazor.Models.BoardPoint;
using BackgammonBlazor.Models.Checker;
using BackgammonBlazor.Models.Dice;
using BackgammonBlazor.Models.Player;

namespace BackgammonBlazor.Models.Game
{
    public class GameInitializer()
    {
        private GameModel _game;
        private static readonly Random _random = new();

        public void Initialize(GameModel game)
        {
            _game = game;

            InitializeDice();
            InitializePlayers();
            InitializeFirstTurn();
            InitializePoints();
            InitializeCheckers();
        }

        private void InitializeDice()
            => _game.Dice = new DiceModel(_game);

        private void InitializePlayers()
        {
            _game.Players.Add(new PlayerModel(_game, PlayerColor.Light));
            _game.Players.Add(new PlayerModel(_game, PlayerColor.Dark));

            GetRandomPlayer().IsActivePlayer = true;

            _game.Hero = _game.Players.First(p => p.IsActivePlayer);
            _game.Villain = _game.Players.First(p => !p.IsActivePlayer);
        }

        private PlayerModel GetRandomPlayer()
            => _game.Players[_random.Next(0, 2)];

        private void InitializeFirstTurn()
            => _game.StartNewTurn();

        private void InitializePoints()
        {
            for (int i = 1; i <= 24; i++)
            {
                _game.Points.Add(i, new BoardPointModel(_game, i));
            }

            //Borne Off Points
            _game.Points.Add((int)BorneOffPoint.Light, new BoardPointModel(_game, (int)BorneOffPoint.Light)); //PlayerColor.Light BorneOff
            _game.Points.Add((int)BorneOffPoint.Dark, new BoardPointModel(_game, (int)BorneOffPoint.Dark)); //PlayerColor.Dark BorneOff

            //Bar Points
            _game.Points.Add((int)PlayerColor.Light, new BoardPointModel(_game, (int)PlayerColor.Light)); //PlayerColor.Light
            _game.Points.Add((int)PlayerColor.Dark, new BoardPointModel(_game, (int)PlayerColor.Dark)); //PlayerColor.Dark

        }

        private void InitializeCheckers()
        {
            foreach (var (pointNumber, numCheckers, playerColor) in InitialCheckerInfo)
            {
                for (int i = 0; i < numCheckers; i++)
                {
                    BoardPointModel point = _game.GetPoint(pointNumber);

                    CheckerModel checker = new(_game, point, playerColor);

                    point.Checkers.Add(checker);

                    _game.Checkers.Add(checker);
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

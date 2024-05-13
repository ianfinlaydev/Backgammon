using BackgammonBlazor.Models;
using System.Drawing;

namespace BackgammonBlazor.Helpers
{
    public static class GameInitializer
    {
        public static void InitializeGame(this GameModel game)
        {
            game.InitializePlayers();
            game.InitializePoints();
            game.InitializeCheckers();
        }

        public static void InitializePlayers(this GameModel game)
        {
            game.Players[0] = new PlayerModel
            {
                PlayerColor = PlayerColor.Light,
            };
            game.Players[1] = new PlayerModel
            {
                PlayerColor = PlayerColor.Dark,
            };

            PlayerColor firstTurnColor = GetRandomPlayerColor();
            game.ChangeActivePlayer(firstTurnColor);
        }

        private static PlayerColor GetRandomPlayerColor()
            => new Random().Next(0, 2) == 1 ? 
            PlayerColor.Light : 
            PlayerColor.Dark;

        public static void InitializePoints(this GameModel game)
        {
            //Points 1 - 24
            for (int i = 1; i <= 24; i++)
            {
                game.AddPoint(i);
            }

            //Bar Points
            game.AddPoint(PlayerColor.Light);
            game.AddPoint(PlayerColor.Dark);
        }

        public static void InitializeCheckers(this GameModel game)
        {
            foreach (var (pointNumber, numCheckers, checkerColor) in InitialCheckers)
            {
                for (int i = 0; i < numCheckers; i++)
                {
                    game.AddChecker(pointNumber, checkerColor);
                }
            }
        }

        public static void AddPoint(this GameModel game, int pointNumber)
            => game.Points.Add(pointNumber, new BoardPointModel
            {
                PointNumber = pointNumber,
            });

        public static void AddPoint(this GameModel game, PlayerColor playerColor)
            => game.AddPoint((int)playerColor);

        public static void AddChecker(this GameModel game, int pointNumber, CheckerColor checkerColor)
            => game.GetPoint(pointNumber).Checkers.Add(new CheckerModel
            {
                Point = game.GetPoint(pointNumber),
                CheckerColor = checkerColor,
            });

        private static readonly (int pointNumber, int numCheckers, CheckerColor checkerColor)[] InitialCheckers =
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

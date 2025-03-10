using BackgammonBlazor.Components.BackgammonComponents;
using BackgammonBlazor.Models;

namespace BackgammonBlazor.Helpers
{
    public static class ColorHelper
    {
        private static bool EqualsEnum(this PlayerColor playerColor, CheckerColor checkerColor)
            => (int)playerColor == (int)checkerColor;

        private static bool EqualsEnum(this CheckerColor checkerColor, PlayerColor playerColor)
            => (int)checkerColor == (int)playerColor;

        public static bool Matches(this PlayerModel player, CheckerModel checker)
            => player.PlayerColor.EqualsEnum(checker.CheckerColor);

        public static bool MatchesPlayer(this CheckerModel checker, PlayerModel player)
            => checker.CheckerColor.EqualsEnum(player.PlayerColor);
    }
}

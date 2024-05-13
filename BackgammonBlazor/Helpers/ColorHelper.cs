using BackgammonBlazor.Models;

namespace BackgammonBlazor.Helpers
{
    public static class ColorHelper
    {
        public static bool EqualsEnum(this PlayerColor playerColor, CheckerColor checkerColor)
            => (int)playerColor == (int)checkerColor;

        public static bool EqualsEnum(this CheckerColor checkerColor, PlayerColor playerColor)
            => (int)checkerColor == (int)playerColor;
    }
}

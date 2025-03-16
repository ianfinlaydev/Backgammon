using BackgammonBlazor.Models;

namespace BackgammonBlazor.Models.BoardPoint
{
    //TODO: Better location for this?
    public static class BoardPointHelper
    {
        private static readonly int[] _pointOrder = [13, 14, 15, 16, 17, 18, 12, 11, 10, 9, 8, 7, 19, 20, 21, 22, 23, 24, 6, 5, 4, 3, 2, 1];

        private static int _index = 0;

        public static int GetNextPointNumber()
        {
            if (_index >= _pointOrder.Length)
            {
                _index = 0;
            }

            return _pointOrder[_index++];
        }
    }
}

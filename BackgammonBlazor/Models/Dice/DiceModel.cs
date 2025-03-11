using BackgammonBlazor.Models.Game;

namespace BackgammonBlazor.Models.Dice
{
    public class DiceModel(GameModel gameModel)
    {
        #region Public Properties
        public GameModel GameModel { get; set; } = gameModel;

        public List<int> Values { get; private set; } = [];
        #endregion Public Properties

        private List<int> _usedValues = [];
        private readonly Random _random = new();
        private bool _isDouble;

        public void Roll()
        {
            Values.Clear();

            //Generate dice values
            Values.AddRange([GetRandomValue(), GetRandomValue()]);

            _isDouble = Values[0] == Values[1];

            if (_isDouble) //A double dice roll gets x4 the rolled value.
            {
                Values.AddRange([Values[0], Values[0]]);
            }

            SortDice();
        }

        public void UseDiceValue(int value)
        {
            _usedValues.Add(value);
            Values.Remove(value);
        }

        public void UnuseDiceValue(int value)
        {
            Values.Add(value);
            _usedValues.Remove(value);

            SortDice();
        }

        public void SwapValues()
        {
            if (_isDouble || Values.Count != 2)
            {
                return;
            }

            (Values[1], Values[0]) = (Values[0], Values[1]);
        }

        public bool HasValues()
            => Values.Count > 0;

        private int GetRandomValue()
            => _random.Next(1, 7);

        private void SortDice()
            => Values = [.. Values.OrderByDescending(v => v)];
    }
}

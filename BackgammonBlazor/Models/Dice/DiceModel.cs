using BackgammonBlazor.Models.Game;

namespace BackgammonBlazor.Models.Dice
{
    public class DiceModel(GameModel gameModel)
    {
        #region Public Properties
        public GameModel GameModel { get; set; } = gameModel;
        #endregion Public Properties

        private List<int> _unusedValues = [];
        private List<int> _usedValues = [];

        private readonly Random _random = new();

        private bool _isDouble;

        public void Roll()
        {
            _unusedValues.Clear();
            _usedValues.Clear();

            //Generate dice values
            _unusedValues.AddRange([GetRandomValue(), GetRandomValue()]);

            _isDouble = _unusedValues[0] == _unusedValues[1];

            if (_isDouble) //A double dice roll gets x4 the rolled value.
            {
                _unusedValues.AddRange([_unusedValues[0], _unusedValues[0]]);
            }
        }

        public void UseDiceValue(int value)
        {
            _unusedValues.Remove(value);
            _usedValues.Add(value);
        }

        public void UnuseDiceValue(int value)
        {
            _usedValues.Remove(value);
            _unusedValues.Insert(0, value);
        }

        public bool HasUnusedValues()
            => _unusedValues.Count > 0;

        private int GetRandomValue()
            => _random.Next(1, 7);

        public IEnumerable<int> GetAllValues() => _unusedValues.Concat(_usedValues);

        public IEnumerable<int> GetUnusedValues() => _unusedValues;

        public IEnumerable<int> GetUsedValues() => _usedValues;
    }
}

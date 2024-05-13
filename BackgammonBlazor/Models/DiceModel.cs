namespace BackgammonBlazor.Models
{
    public class DiceModel
    {
        public List<int> Values { get; set; } = [];

        private readonly Random _random = new();
        private bool _isDouble;

        public void Roll()
        {
            Values.Clear();

            //Add initial 2 values from dice roll
            for (int i = 0; i < 2; i++)
            {
                int value = GetRandomtValue();
                Values.Add(value);
            }

            _isDouble = Values[0] == Values[1];

            if (_isDouble) //A double dice roll gets 4x the rolled value.
            {
                Values.AddRange(Values);
            }
            else //If not a double dice roll, sort the values in descending order
            {
                Values = [.. Values.OrderByDescending(v => v)];
            }
        }

        public void Swap()
        {
            if (_isDouble || Values.Count != 2)
            {
                return;
            }

            (Values[1], Values[0]) = (Values[0], Values[1]);
        }

        private int GetRandomtValue()
            => _random.Next(1, 7);
    }
}

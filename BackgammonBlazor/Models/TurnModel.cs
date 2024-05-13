namespace BackgammonBlazor.Models
{
    public class TurnModel()
    {
        //TODO: Can dice be moved from player to TurnModel so we don't need to pass the dice values?
        private readonly Stack<MoveModel> _moves = [];
        private readonly List<int> _diceValues = [];
        private bool _isDouble;

        public void AddMove(MoveModel move)
        {
            //TODO: once dice are moved from player to turnmodel:
            //  _diceValues.Remove(Math.Abs(move.Origin - move.Destination)) or something like that
            _moves.Push(move);
            _diceValues.Remove(Math.Abs(move.Destination.PointNumber - move.Origin.PointNumber));
        }

        public MoveModel UndoMove()
        {
            MoveModel move = _moves.Pop();
            (move.Origin, move.Destination) = (move.Destination, move.Origin);
            return move;
        }

        public void AddDiceValues(List<int> values)
        {
            _diceValues.AddRange(values);
            _isDouble = values.Count == 4;
        }

        public bool HasMoved() 
            => _moves.Count > 0;

        public bool IsComplete() 
            => _diceValues != null && _diceValues.Count <= 0;

        public bool IsDouble() 
            => _isDouble;
    }
}

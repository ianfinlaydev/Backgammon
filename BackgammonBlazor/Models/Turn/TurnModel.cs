using BackgammonBlazor.Components.BackgammonComponents;
using BackgammonBlazor.Models.Game;
using BackgammonBlazor.Models.Move;

namespace BackgammonBlazor.Models.Turn
{
    public class TurnModel(GameModel gameModel)
    {
        public GameModel GameModel { get; set; } = gameModel;

        private readonly Stack<MoveModel> _moves = [];

        public void AddMove(MoveModel move)
        {
            _moves.Push(move);
        }

        public MoveModel GetLastMove()
            => _moves.Peek();

        public void RemoveLastMove()
            => _moves.Pop();

        public bool HasMoved()
            => _moves.Count > 0;

        public bool IsComplete()
            => !GameModel.Dice.HasValues();
    }
}

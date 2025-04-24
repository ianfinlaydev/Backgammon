using BackgammonBlazor.Models.Game;
using BackgammonBlazor.Models.Player;
using System.Xml.Linq;

namespace BackgammonBlazor.Models.Move
{
    public class MoveValidator(GameModel game)
    {
        private GameModel _game = game;
        private MoveModel _move;

        public bool IsValidMove(MoveModel move)
        {
            _move = move;

            //Hero needs to bear on checkers from their bar before making another move
            if (_game.Hero.HasCheckersOnBar() && _move.Origin != _game.GetBarPoint(_game.Hero.PlayerColor))
            {
                return false;
            }

            //Move is invalid because destination point is made by villain
            if (move.Destination.IsMadeByPlayer(_game.Villain))
            {
                return false;
            }

            //Hero is able to bear off but specific move is invalid
            if (IsBearingOff() && !IsValidBearOff())
            {
                return false;
            }

            return true;
        }

        internal bool IsValidBearOff()
        {
            if (!_game.Hero.HasAllCheckersInHomeBoard())
            {
                return false;
            }

            if (IsPerfectBearOff())
            {
                return true;
            }

            //TODO: move this Method to this class
            if (_game.Hero.CanMoveCheckerOnHigherPointNumber(_move.Origin.PointNumber, _move.ConsumedDiceValue))
            {
                return false;
            }

            return true;
        }

        private bool IsPerfectBearOff()
            => _move.ConsumedDiceValue == (_game.Hero.PlayerColor == PlayerColor.Light ? _move.Origin.PointNumber : 25 - _move.Origin.PointNumber);

        public bool IsBearingOff()
            => _move.Destination == _game.GetBorneOffPoint(PlayerColor.Light) ||
            _move.Destination == _game.GetBorneOffPoint(PlayerColor.Dark);
    }
}

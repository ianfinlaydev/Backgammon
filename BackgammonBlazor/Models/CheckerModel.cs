using System.ComponentModel.DataAnnotations;

namespace BackgammonBlazor.Models
{
    public class CheckerModel
    {
        public BoardPointModel? Point { get; set; }

        public CheckerColor CheckerColor { get; set; }

        public void Move(BoardPointModel? origin, BoardPointModel? destination)
        {
            if (origin == null || destination == null)
            {
                throw new ArgumentNullException(origin == null ? nameof(origin) : nameof(destination));
            }

            Point = destination;
            origin.Checkers.Remove(this);
            destination.Checkers.Add(this);
        }
    }

    public enum CheckerColor
    {
        Light = int.MinValue,
        Dark = int.MaxValue,
    }
}

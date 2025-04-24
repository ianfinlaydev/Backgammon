using BackgammonBlazor.Models.Point;
using BackgammonBlazor.Models.Game;
using System.ComponentModel.DataAnnotations;

namespace BackgammonBlazor.Models.Move
{
    public class MoveFactory(IServiceProvider serviceProvider)
    {
        private readonly IServiceProvider _serviceProvider = serviceProvider;

        public MoveModel CreateMove(PointModel origin, int consumedDiceValue)
            => new(_serviceProvider.GetRequiredService<GameModel>(), consumedDiceValue, origin);
    }
}

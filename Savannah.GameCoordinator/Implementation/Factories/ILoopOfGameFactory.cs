using Savannah.FieldOfGame;
using Savannah.GameCoordinator.Loop;

namespace Savannah.GameCoordinator.Factories
{
    public interface ILoopOfGameFactory
    {
        ILoopOfGame GetLoopOfGame(IGameField gameField);
    }
}
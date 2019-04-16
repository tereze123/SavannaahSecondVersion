using Savannah.FieldOfGame;

namespace Savannah.GameCoordinator.Loop
{
    public interface ILoopOfGame
    {
        void UsersTurnToAddAnimals(IGameField gameField, string userKeyPressed);
        void LoopThroughTheGame();
    }
}
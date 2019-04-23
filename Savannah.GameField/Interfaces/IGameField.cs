using AccessLibraryForPlugins.Animals;
using System.Collections.Generic;

namespace Savannah.FieldOfGame
{
    public interface IGameField
    {
        IAnimal[,] GameState { get; set; }

        IAnimal[,] CreateNewGameState();
        List<AccessLibraryForPlugins.PositionOnField> GetAllFreePositionsOnField();
        int GetGameFieldSize();
        AccessLibraryForPlugins.PositionOnField GetRandomAndFreePositionOnField();
    }
}
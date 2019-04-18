using Savannaah.Animals;
using System.Collections.Generic;

namespace Savannah.FieldOfGame
{
    public interface IGameField
    {
        IAnimal[,] GameState { get; set; }

        IAnimal[,] CreateNewGameState();
        List<PositionOnField.PositionOnField> GetAllFreePositionsOnField();
        int GetGameFieldSize();
        PositionOnField.PositionOnField GetRandomAndFreePositionOnField();
    }
}
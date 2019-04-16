using Savannaah.Animals;
using System.Collections.Generic;

namespace Savannah.FieldOfGame
{
    public interface IGameField
    {
        Animal[,] GameState { get; set; }

        Animal[,] CreateNewGameState();
        List<PositionOnField.PositionOnField> GetAllFreePositionsOnField();
        int GetGameFieldSize();
        PositionOnField.PositionOnField GetRandomAndFreePositionOnField();
    }
}
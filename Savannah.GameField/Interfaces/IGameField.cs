using System.Collections.Generic;
using Savannaah.Animals;
using Savannah.PositionOnField;

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
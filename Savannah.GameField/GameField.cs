using Savannaah.Animals;
using Savannah.PositionOnField;
using System.Collections.Generic;

namespace Savannah.FieldOfGame
{
    public class GameField
    {
        public Animal[,] GameState { get; set; }

        private readonly PositionOnFieldValidation positionOnFieldValidation;

        public GameField(PositionOnFieldValidation positionOnFieldValidation)
        {
            GameState = new Animal[this.GetGameFieldSize(), this.GetGameFieldSize()];
            this.positionOnFieldValidation = positionOnFieldValidation;
        }

        public Animal[,] CreateNewGameState()
        {
            return new Animal[this.GetGameFieldSize(), this.GetGameFieldSize()];
        }

        public int GetGameFieldSize()
        {
            return 10;
        }

        public List<PositionOnField.PositionOnField> GetAllFreePositionsOnField()
        {
            var freePositionList = new List<PositionOnField.PositionOnField>();

            for (int row = 0; row < this.GetGameFieldSize(); row++)
            {
                for (int column = 0; column < this.GetGameFieldSize(); column++)
                {
                    if (this.CheckIfThisPositionIsFree(GameState, row, column))
                    {
                        freePositionList.Add(new PositionOnField.PositionOnField(row, column));
                    }
                }
            }
            return freePositionList;
        }

        private bool CheckIfThisPositionIsFree(
            Animal[,] gameField,
            int rowPosition,
            int columnPosition)
        {
            return (gameField[rowPosition, columnPosition] == null) ? true : false;
        }
    }
}

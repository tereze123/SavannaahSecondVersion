using System;
using System.Collections.Generic;
using System.Linq;
using Savannah.PositionOnField;
namespace Savannaah.Animals
{
    public abstract class Animal
    {
        private readonly PositionOnFieldValidation positionOnFieldValidation;

        public Animal()
        {
            this.positionOnFieldValidation = new PositionOnFieldValidation(new Savannah.Common.Configuration());
        }


        public string Name { get; set; }

        public int VisionRange { get; set; }

        public virtual void PeaceStateMovementNextPosition(
            Animal[,] initialGeneration,
            Animal[,] nextGenerationArray,
            int rowPosition,
            int columnPosition
            )
        {

            var freePositions = this.GetFreePositionsAroundAnimal(initialGeneration, nextGenerationArray, rowPosition, columnPosition);
            bool freePositionsAreAvailable = !freePositions.Any();

            if (freePositionsAreAvailable)
            {
                Random random = new Random();
                var freePositionNumberFromTheList = random.Next(0, freePositions.Count);
                PositionOnField newAnimalsPositionOnField = freePositions.ElementAt(freePositionNumberFromTheList);
                nextGenerationArray[newAnimalsPositionOnField.RowPosition, newAnimalsPositionOnField.ColumnPosition] = this;
            }
        }

        private List<PositionOnField> GetFreePositionsAroundAnimal(
            Animal[,] initialGeneration,
            Animal[,] nextGenerationArray,
            int rowPosition,
            int columnPosition
            )
        {
            var freePositionList = new List<PositionOnField>();

            for (int row = -1; row < 2; row++)
            {
                if (row == 0)
                {
                    continue;
                }
                if (!(positionOnFieldValidation.IsOutOfBounds(rowPosition + row, columnPosition))
                    && CheckIfThisPositionIsFree(initialGeneration, rowPosition + row, columnPosition)
                    && CheckIfThisPositionIsFree(nextGenerationArray, rowPosition + row, columnPosition)
                    )
                {
                    freePositionList.Add(new PositionOnField(rowPosition + row, columnPosition));
                }
            }

            for (int column = -1; column < 2; column++)
            {
                if (column == 0)
                {
                    continue;
                }
                if (!(positionOnFieldValidation.IsOutOfBounds(rowPosition, columnPosition + column))
                    && CheckIfThisPositionIsFree(initialGeneration, rowPosition, columnPosition + column)
                    && CheckIfThisPositionIsFree(nextGenerationArray, rowPosition, columnPosition + column)
                   )
                {
                    freePositionList.Add(new PositionOnField(rowPosition, columnPosition + column));
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

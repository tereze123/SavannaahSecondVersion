using Savannah.Common;
using Savannah.PositionOnField;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Savannaah.Animals
{
    public abstract class Animal
    {
        private readonly Configuration configuration;
        private readonly PositionOnFieldValidation positionOnFieldValidation;



        public Animal()
        {
            positionOnFieldValidation = new PositionOnFieldValidation(new Savannah.Common.Configuration());
            configuration = new Configuration();

        }

        public string EnemiesName { get; set; }

        public string Name { get; set; }

        public int VisionRange { get; set; }

        public virtual PositionOnField EnemysPositionOnField(Animal[,] initialGeneration, int rowPosition, int columnPosition)
        {
            PositionOnField positionOnField = new PositionOnField();
            for (int rowsInVisionRange = VisionRange * -1; rowsInVisionRange <= VisionRange; rowsInVisionRange++)
            {
                for (int columnsInVisionRange = VisionRange * -1; columnsInVisionRange <= VisionRange; columnsInVisionRange++)
                {
                    var rowToCheck = rowsInVisionRange + rowPosition;
                    var columnToCheck = columnsInVisionRange + columnPosition;
                    if (RowAndColumnAreBiggerThanZero(rowToCheck, columnToCheck))
                    {
                        if (ThisCellIsValidAndContainsEnemy(initialGeneration, rowToCheck, columnToCheck))
                        {
                            positionOnField.ColumnPosition = columnPosition + columnsInVisionRange;
                            positionOnField.RowPosition = rowPosition + rowsInVisionRange;
                            positionOnField.IsEnemyInViewRange = true;
                            return positionOnField;
                        }
                    }
                }
            }
            positionOnField.IsEnemyInViewRange = false;
            return positionOnField;
        }

        private bool RowAndColumnAreBiggerThanZero(int rowToCheck, int columnToCheck)
        {
            return (rowToCheck >= 0 && columnToCheck >= 0) ? true : false;
        }

        private bool ThisCellIsNotNull(Animal[,] initialGeneration, int rowToCheck, int columnToCheck)
        {
            return (initialGeneration[rowToCheck, columnToCheck] != null) ? true : false;
        }

        private bool ThisCellIsValidAndContainsEnemy(Animal[,] initialGeneration, int rowToCheck, int columnToCheck)
        {
            if (
                !(positionOnFieldValidation.IsOutOfBounds(rowToCheck, columnToCheck))
                && ThisCellIsNotNull(initialGeneration, rowToCheck, columnToCheck)
                && ThisCellContainsEnemy(initialGeneration, rowToCheck, columnToCheck)
                )
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        private bool ThisCellContainsEnemy(Animal[,] initialGeneration, int rowToCheck, int columnToCheck)
        {
            return (initialGeneration[rowToCheck, columnToCheck].Name == EnemiesName) ? true : false;
        }

        public virtual void EnemyIsInRangeMovementNextPosition(
                Animal[,] initialGeneration,
                Animal[,] nextGenerationArray,
                int rowPositionOfEnemy,
                int columnPositionOfEnemy,
                int rowPositionOfAnimal,
                int columnPositionOfAnimal
    )
        {
            var allFreePositions = GetFreePositionsAroundAnimal(
                initialGeneration,
                nextGenerationArray,
                rowPositionOfAnimal,
                columnPositionOfAnimal);


            var positionsWhereAnimalCanRunAway = new List<PositionOnField>();



            if (EnemyIsLowerThanAnimal(rowPositionOfAnimal, rowPositionOfEnemy))
            {
                positionsWhereAnimalCanRunAway.AddRange(
                                            allFreePositions
                                            .Where(p => p.RowPosition < rowPositionOfAnimal)
                                            );
            }
            else
            {
                positionsWhereAnimalCanRunAway.AddRange(
                                                allFreePositions
                                                .Where(p => p.RowPosition > rowPositionOfAnimal));
            }
            if (EnemyIsToTheRightOfAnimal(columnPositionOfAnimal, columnPositionOfEnemy))
            {
                positionsWhereAnimalCanRunAway.AddRange(
                                                allFreePositions
                                                .Where(p => p.ColumnPosition < columnPositionOfAnimal));
            }
            else
            {
                positionsWhereAnimalCanRunAway.AddRange(
                                                allFreePositions
                                                .Where(p => p.ColumnPosition > columnPositionOfAnimal));
            }

            bool freePositionsAreAvailable = positionsWhereAnimalCanRunAway.Any();

            if (freePositionsAreAvailable)
            {
                Random random = new Random();
                var freePositionNumberFromTheList = random.Next(0, positionsWhereAnimalCanRunAway.Count());
                PositionOnField newAnimalsPositionOnField = positionsWhereAnimalCanRunAway.ElementAt(freePositionNumberFromTheList);
                nextGenerationArray[newAnimalsPositionOnField.RowPosition, newAnimalsPositionOnField.ColumnPosition] = this;
            }
            else
            {
                nextGenerationArray[rowPositionOfAnimal, columnPositionOfAnimal] = this;
            }
        }

        private bool EnemyIsLowerThanAnimal(int rowPositionAnimal, int rowPositionEnemy)
        {
            return (rowPositionEnemy > rowPositionAnimal) ? true : false;
        }

        private bool EnemyIsToTheRightOfAnimal(int columnPositionAnimal, int columnPositionEnemy)
        {
            return (columnPositionEnemy > columnPositionAnimal) ? true : false;
        }

        public virtual void PeaceStateMovementNextPosition(
            Animal[,] initialGeneration,
            Animal[,] nextGenerationArray,
            int rowPosition,
            int columnPosition
            )
        {

            var freePositions = GetFreePositionsAroundAnimal(initialGeneration, nextGenerationArray, rowPosition, columnPosition);
            bool freePositionsAreAvailable = freePositions.Any();

            if (freePositionsAreAvailable)
            {
                Random random = new Random();
                var freePositionNumberFromTheList = random.Next(0, freePositions.Count);
                PositionOnField newAnimalsPositionOnField = freePositions.ElementAt(freePositionNumberFromTheList);
                nextGenerationArray[newAnimalsPositionOnField.RowPosition, newAnimalsPositionOnField.ColumnPosition] = this;
            }
            else
            {
                nextGenerationArray[rowPosition, columnPosition] = this;
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

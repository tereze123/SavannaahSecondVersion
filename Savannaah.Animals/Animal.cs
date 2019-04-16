using Savannah.Common;
using Savannah.Common.Factories;
using Savannah.PositionOnField;
using Savannah.PositionOnField.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Savannaah.Animals
{
    public abstract class Animal
    {
        private readonly IConfiguration configuration;
        private readonly IPositionOnFieldValidation positionOnFieldValidation;
        protected readonly IPositionOnFieldFactory positionOnFieldFactory;
        private readonly IRandomiserFactory randomiserFactory;

        public Animal(IConfiguration configuration, 
            IPositionOnFieldValidation positionOnFieldValidation, 
            IPositionOnFieldFactory positionOnFieldFactory,
            IRandomiserFactory randomiserFactory)
        {
            this.configuration = configuration;
            this.positionOnFieldValidation = positionOnFieldValidation;
            this.positionOnFieldFactory = positionOnFieldFactory;
            this.randomiserFactory = randomiserFactory;
        }

        public string EnemiesName { get; set; }

        public string Name { get; set; }

        public int VisionRange { get; set; }

        public virtual PositionOnField EnemysPositionOnField(Animal[,] initialGeneration, int rowPosition, int columnPosition)
        {
            PositionOnField positionOnField = positionOnFieldFactory.GetNewPositionOnField(); 
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

            var positionsWhereAnimalCanRunAway = positionOnFieldFactory.GetNewListOfPositionsOnField(); 

            if (EnemyIsLowerThanAnimal(rowPositionOfAnimal, rowPositionOfEnemy))
            {
                positionsWhereAnimalCanRunAway.AddRange(
                                            allFreePositions
                                            .Where(p => p.RowPosition < rowPositionOfAnimal));
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

            MakeAmoveIntoNextGeneration(nextGenerationArray, rowPositionOfAnimal, columnPositionOfAnimal, positionsWhereAnimalCanRunAway);
        }

        protected void MakeAmoveIntoNextGeneration(
            Animal[,] nextGenerationArray,
            int rowPositionOfAnimal,
            int columnPositionOfAnimal,
            List<PositionOnField> positionsWhereAnimalCanRunAway)
        {
            bool freePositionsAreAvailable = positionsWhereAnimalCanRunAway.Any();

            if (freePositionsAreAvailable)
            {
                var random = randomiserFactory.GetRandom();
                var freePositionNumberFromTheList = random.Next(0, positionsWhereAnimalCanRunAway.Count());
                PositionOnField newAnimalsPositionOnField = positionsWhereAnimalCanRunAway.ElementAt(freePositionNumberFromTheList);

                nextGenerationArray[newAnimalsPositionOnField.RowPosition, newAnimalsPositionOnField.ColumnPosition] = this;
            }

            else
            {
                if (nextGenerationArray[rowPositionOfAnimal, columnPositionOfAnimal] == null)
                {
                    nextGenerationArray[rowPositionOfAnimal, columnPositionOfAnimal] = this;
                }
            }
        }

        protected bool EnemyIsLowerThanAnimal(int rowPositionAnimal, int rowPositionEnemy)
        {
            return (rowPositionEnemy > rowPositionAnimal) ? true : false;
        }

        protected bool EnemyIsToTheRightOfAnimal(int columnPositionAnimal, int columnPositionEnemy)
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
                var random = randomiserFactory.GetRandom();
                var freePositionNumberFromTheList = random.Next(0, freePositions.Count);
                PositionOnField newAnimalsPositionOnField = freePositions.ElementAt(freePositionNumberFromTheList);
                nextGenerationArray[newAnimalsPositionOnField.RowPosition, newAnimalsPositionOnField.ColumnPosition] = this;
            }
            else
            {
                nextGenerationArray[rowPosition, columnPosition] = this;
            }
        }

        protected List<PositionOnField> GetFreePositionsAroundAnimal(
            Animal[,] initialGeneration,
            Animal[,] nextGenerationArray,
            int rowPosition,
            int columnPosition
            )
        {
            var freePositionList = positionOnFieldFactory.GetNewListOfPositionsOnField(); 

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
                    freePositionList.Add(positionOnFieldFactory.GetNewPositionOnField(rowPosition + row, columnPosition));
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
                    freePositionList.Add(positionOnFieldFactory.GetNewPositionOnField(rowPosition, columnPosition + column));
                }
            }
            return freePositionList;
        }

        protected bool CheckIfThisPositionIsFree(
            Animal[,] gameField,
            int rowPosition,
            int columnPosition)
        {
            if (Name == configuration.GetNameOfLion())
            {
                if ((gameField[rowPosition, columnPosition] == null)
                    || EnemyIsHere(gameField, rowPosition, columnPosition))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return (gameField[rowPosition, columnPosition] == null) ? true : false;
            }
        }

        private bool EnemyIsHere(Animal[,] gameField, int rowPosition, int columnPosition)
        {
            return (gameField[rowPosition, columnPosition].Name == EnemiesName);
        }
    }
}

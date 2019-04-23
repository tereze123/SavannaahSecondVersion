using Savannaah.Animals;
using Savannah.Common;
using Savannah.Common.Factories;
using Savannah.PositionOnField;
using Savannah.PositionOnField.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Savannah.GameCoordinator.GameLogic
{
    public class SavannahGameLogic
    {
        private readonly IConfiguration configuration;
        private readonly IPositionOnFieldValidation positionOnFieldValidation;
        private readonly IPositionOnFieldFactory positionOnFieldFactory;
        private readonly IRandomiserFactory randomiserFactory;

        public SavannahGameLogic(IConfiguration configuration,
            IPositionOnFieldValidation positionOnFieldValidation,
            IPositionOnFieldFactory positionOnFieldFactory,
            IRandomiserFactory randomiserFactory)
        {
            this.configuration = configuration;
            this.positionOnFieldValidation = positionOnFieldValidation;
            this.positionOnFieldFactory = positionOnFieldFactory;
            this.randomiserFactory = randomiserFactory;
        }

        public virtual AccessLibraryForPlugins.PositionOnField EnemysPositionOnField(Animal[,] initialGeneration, 
            int rowPosition, 
            int columnPosition,
            int visionRange,
            string enemiesName)
        {
            AccessLibraryForPlugins.PositionOnField positionOnField = positionOnFieldFactory.GetNewPositionOnField();
            for (int rowsInVisionRange = visionRange * -1; rowsInVisionRange <= visionRange; rowsInVisionRange++)
            {
                for (int columnsInVisionRange = visionRange * -1; columnsInVisionRange <= visionRange; columnsInVisionRange++)
                {
                    var rowToCheck = rowsInVisionRange + rowPosition;
                    var columnToCheck = columnsInVisionRange + columnPosition;
                    if (RowAndColumnAreBiggerThanZero(rowToCheck, columnToCheck))
                    {
                        if (ThisCellIsValidAndContainsEnemy(initialGeneration, rowToCheck, columnToCheck, enemiesName))
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
            return (rowToCheck >= 0 && columnToCheck >= 0);
        }

        private bool ThisCellIsNotNull(Animal[,] initialGeneration, int rowToCheck, int columnToCheck)
        {
            return (initialGeneration[rowToCheck, columnToCheck] != null) ? true : false;
        }

        private bool ThisCellIsValidAndContainsEnemy(Animal[,] initialGeneration, 
                                                    int rowToCheck, 
                                                    int columnToCheck, 
                                                    string enemiesName)
        {
            if (
                !(positionOnFieldValidation.IsOutOfBounds(rowToCheck, columnToCheck))
                && ThisCellIsNotNull(initialGeneration, rowToCheck, columnToCheck)
                && ThisCellContainsEnemy(initialGeneration, rowToCheck, columnToCheck, enemiesName)
                )
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        private bool ThisCellContainsEnemy(Animal[,] initialGeneration, 
                                           int rowToCheck, 
                                           int columnToCheck,
                                           string enemiesName)
        {
            return (initialGeneration[rowToCheck, columnToCheck].Name == enemiesName) ? true : false;
        }

        public virtual void EnemyIsInRangeMovementNextPosition(
                Animal[,] initialGeneration,
                Animal[,] nextGenerationArray,
                int rowPositionOfEnemy,
                int columnPositionOfEnemy,
                int rowPositionOfAnimal,
                int columnPositionOfAnimal,
                Animal animal
    )
        {
            var allFreePositions = GetFreePositionsAroundAnimal(
                initialGeneration,
                nextGenerationArray,
                rowPositionOfAnimal,
                columnPositionOfAnimal,
                animal);

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

            MakeAmoveIntoNextGeneration(nextGenerationArray, rowPositionOfAnimal, columnPositionOfAnimal, positionsWhereAnimalCanRunAway, animal);
        }

        protected void MakeAmoveIntoNextGeneration(
            Animal[,] nextGenerationArray,
            int rowPositionOfAnimal,
            int columnPositionOfAnimal,
            List<AccessLibraryForPlugins.PositionOnField> positionsWhereAnimalCanRunAway,
            Animal animal)
        {
            bool freePositionsAreAvailable = positionsWhereAnimalCanRunAway.Any();

            if (freePositionsAreAvailable)
            {
                var random = randomiserFactory.GetRandom();
                var freePositionNumberFromTheList = random.Next(0, positionsWhereAnimalCanRunAway.Count());
                AccessLibraryForPlugins.PositionOnField newAnimalsPositionOnField = positionsWhereAnimalCanRunAway.ElementAt(freePositionNumberFromTheList);

                nextGenerationArray[newAnimalsPositionOnField.RowPosition, newAnimalsPositionOnField.ColumnPosition] = animal;
            }

            else
            {
                if (nextGenerationArray[rowPositionOfAnimal, columnPositionOfAnimal] == null)
                {
                    nextGenerationArray[rowPositionOfAnimal, columnPositionOfAnimal] = animal;
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
            int columnPosition,
            Animal animal
            )
        {

            var freePositions = GetFreePositionsAroundAnimal(initialGeneration, 
                nextGenerationArray, 
                rowPosition, 
                columnPosition,
                animal);
            bool freePositionsAreAvailable = freePositions.Any();

            if (freePositionsAreAvailable)
            {
                var random = randomiserFactory.GetRandom();
                var freePositionNumberFromTheList = random.Next(0, freePositions.Count);
                AccessLibraryForPlugins.PositionOnField newAnimalsPositionOnField = freePositions.ElementAt(freePositionNumberFromTheList);
                nextGenerationArray[newAnimalsPositionOnField.RowPosition, newAnimalsPositionOnField.ColumnPosition] = animal;
            }
            else
            {
                nextGenerationArray[rowPosition, columnPosition] = animal;
            }
        }

        protected List<AccessLibraryForPlugins.PositionOnField> GetFreePositionsAroundAnimal(
            Animal[,] initialGeneration,
            Animal[,] nextGenerationArray,
            int rowPosition,
            int columnPosition,
            Animal animal
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
                    && CheckIfThisPositionIsFree(initialGeneration, rowPosition + row, columnPosition, animal)
                    && CheckIfThisPositionIsFree(nextGenerationArray, rowPosition + row, columnPosition, animal)
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
                    && CheckIfThisPositionIsFree(initialGeneration, rowPosition, columnPosition + column, animal)
                    && CheckIfThisPositionIsFree(nextGenerationArray, rowPosition, columnPosition + column, animal)
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
            int columnPosition,
            Animal animal)
        {
            if (animal.Name == configuration.GetNameOfLion())
            {
                if ((gameField[rowPosition, columnPosition] == null)
                    || EnemyIsHere(gameField, rowPosition, columnPosition, animal.EnemiesName))
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

        private bool EnemyIsHere(Animal[,] gameField, int rowPosition, int columnPosition, string nameOfEnemy)
        {
            return (gameField[rowPosition, columnPosition].Name == nameOfEnemy);
        }

        public void EnemyIsInRangeMovementNextPositionHunter(
                      Animal[,] initialGeneration,
                      Animal[,] nextGenerationArray,
                      int rowPositionOfEnemy,
                      int columnPositionOfEnemy,
                      int rowPositionOfAnimal,
                      int columnPositionOfAnimal,
                      Animal animal )
        {
            var freePositionsOnField = this.GetFreePositionsAroundAnimal(
                                                                         initialGeneration,
                                                                         nextGenerationArray,
                                                                         rowPositionOfAnimal,
                                                                         columnPositionOfAnimal,
                                                                         animal)
                                                                         .ToList();

            var listOfPositionsCanCatchEnemy = positionOnFieldFactory.GetNewListOfPositionsOnField();
            AccessLibraryForPlugins.PositionOnField enemiesPosition = CanEatEnemy(nextGenerationArray, listOfPositionsCanCatchEnemy, animal.EnemiesName);
            if (enemiesPosition != null)
            {
                nextGenerationArray[enemiesPosition.RowPosition, enemiesPosition.ColumnPosition] = animal;
            }
            else
            {

                if (IsEnemyOnTheSameColumn(columnPositionOfAnimal, columnPositionOfEnemy))
                {
                    GetPositionsForSameColumnDifferentRows(
                                        rowPositionOfEnemy,
                                        columnPositionOfEnemy,
                                        rowPositionOfAnimal,
                                        freePositionsOnField,
                                        listOfPositionsCanCatchEnemy);
                }
                else if (IsEnemyOnTheSameRow(rowPositionOfAnimal, rowPositionOfEnemy))
                {
                    GetPositionsForSameRowDifferentColumns(
                        rowPositionOfEnemy,
                        columnPositionOfEnemy,
                        columnPositionOfAnimal,
                        freePositionsOnField,
                        listOfPositionsCanCatchEnemy);
                }
                else
                {
                    if (EnemyIsLowerThanAnimal(rowPositionOfAnimal, rowPositionOfEnemy))
                    {
                        listOfPositionsCanCatchEnemy.AddRange(
                                                    freePositionsOnField
                                                    .Where(p => p.RowPosition > rowPositionOfAnimal)
                                                    );
                    }
                    else
                    {
                        listOfPositionsCanCatchEnemy.AddRange(
                                freePositionsOnField
                                .Where(p => p.RowPosition < rowPositionOfAnimal)
                                );
                    }
                    if (EnemyIsToTheRightOfAnimal(columnPositionOfAnimal, columnPositionOfEnemy))
                    {
                        listOfPositionsCanCatchEnemy.AddRange(
                                                             freePositionsOnField
                                                            .Where(p => p.ColumnPosition > columnPositionOfAnimal));
                    }
                    else
                    {
                        listOfPositionsCanCatchEnemy.AddRange(
                                         freePositionsOnField
                                        .Where(p => p.ColumnPosition < columnPositionOfAnimal));
                    }
                }
                MakeAmoveIntoNextGeneration(nextGenerationArray,
                                                   rowPositionOfAnimal,
                                                   columnPositionOfAnimal,
                                                   listOfPositionsCanCatchEnemy,
                                                   animal);
            }
        }

        private AccessLibraryForPlugins.PositionOnField CanEatEnemy(
            Animal[,] nextGenArray, 
            List<AccessLibraryForPlugins.PositionOnField> listOfPositionsCanCatchEnemy,
            string nameOfEnemy)
        {
            foreach (var position in listOfPositionsCanCatchEnemy)
            {
                if (nextGenArray[position.RowPosition, position.ColumnPosition] != null
                    && nextGenArray[position.RowPosition, position.ColumnPosition].Name == nameOfEnemy)
                {
                    return positionOnFieldFactory.GetNewPositionOnField(position.RowPosition, position.ColumnPosition);
                }
            }
            return null;
        }

        private void GetPositionsForSameRowDifferentColumns(
            int rowPositionOfEnemy,
            int columnPositionOfEnemy,
            int columnPositionOfAnimal,
            List<AccessLibraryForPlugins.PositionOnField> freePositionsOnField,
            List<AccessLibraryForPlugins.PositionOnField> listOfPositionsCanCatchEnemy)
        {
            if (EnemyIsToTheRightOfAnimal(columnPositionOfAnimal, columnPositionOfEnemy))
            {
                listOfPositionsCanCatchEnemy.AddRange(
                                            freePositionsOnField
                                            .Where(p => p.RowPosition == rowPositionOfEnemy && p.ColumnPosition > columnPositionOfAnimal)
                                            );
            }
            else
            {
                listOfPositionsCanCatchEnemy.AddRange(
                        freePositionsOnField
                        .Where(p => p.RowPosition == rowPositionOfEnemy && p.ColumnPosition < columnPositionOfAnimal)
                        );
            }
        }

        private void GetPositionsForSameColumnDifferentRows(
            int rowPositionOfEnemy,
            int columnPositionOfEnemy,
            int rowPositionOfAnimal,
            List<AccessLibraryForPlugins.PositionOnField> freePositionsOnField,
            List<AccessLibraryForPlugins.PositionOnField> listOfPositionsCanCatchEnemy)
        {
            if (EnemyIsLowerThanAnimal(rowPositionOfAnimal, rowPositionOfEnemy))
            {
                listOfPositionsCanCatchEnemy.AddRange(
                                            freePositionsOnField
                                            .Where(p => p.ColumnPosition == columnPositionOfEnemy && p.RowPosition > rowPositionOfAnimal)
                                            );
            }
            else
            {
                listOfPositionsCanCatchEnemy.AddRange(
                        freePositionsOnField
                        .Where(p => p.ColumnPosition == columnPositionOfEnemy && p.RowPosition < rowPositionOfAnimal)
                        );
            }
        }

        private bool IsEnemyOnTheSameRow(int rowPositionOfAnimal, int rowPositionOfEnemy)
        {
            return (rowPositionOfAnimal == rowPositionOfEnemy) ? true : false;
        }

        private bool IsEnemyOnTheSameColumn(int columnPositionOfAnimal, int columnPositionOfEnemy)
        {
            return (columnPositionOfAnimal == columnPositionOfEnemy) ? true : false;
        }

    }
}


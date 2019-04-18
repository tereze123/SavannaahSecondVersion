using Savannaah.Animals;
using Savannah.Common;
using Savannah.Common.Factories;
using Savannah.PositionOnField;
using Savannah.PositionOnField.Factories;
using System.Collections.Generic;
using System.Linq;

namespace Savannah.Animals
{
    public class Lion : Animal
    {
        public Lion(IConfiguration configuration, 
            IPositionOnFieldValidation positionOnFieldValidation,
            IPositionOnFieldFactory positionOnFieldFactory,
            IRandomiserFactory randomiserFactory) 
            : base(configuration, 
                  positionOnFieldValidation, 
                  positionOnFieldFactory,
                  randomiserFactory)
        {
            Name = "L";
            VisionRange = 5;
            EnemiesName = "A";
        }
        public override void EnemyIsInRangeMovementNextPosition(
                            Animal[,] initialGeneration,
                            Animal[,] nextGenerationArray,
                            int rowPositionOfEnemy,
                            int columnPositionOfEnemy,
                            int rowPositionOfAnimal,
                            int columnPositionOfAnimal)
        {
            var freePositionsOnField = base.GetFreePositionsAroundAnimal(
                                                                         initialGeneration,
                                                                         nextGenerationArray,
                                                                         rowPositionOfAnimal,
                                                                         columnPositionOfAnimal)
                                                                         .ToList();

            var listOfPositionsCanCatchEnemy = positionOnFieldFactory.GetNewListOfPositionsOnField();
            PositionOnField.PositionOnField enemiesPosition = CanEatEnemy(nextGenerationArray, listOfPositionsCanCatchEnemy);
            if (enemiesPosition != null)
            {
                nextGenerationArray[enemiesPosition.RowPosition, enemiesPosition.ColumnPosition] = this;
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
                    if (base.EnemyIsLowerThanAnimal(rowPositionOfAnimal, rowPositionOfEnemy))
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
                    if (base.EnemyIsToTheRightOfAnimal(columnPositionOfAnimal, columnPositionOfEnemy))
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
                base.MakeAmoveIntoNextGeneration(nextGenerationArray,
                                                   rowPositionOfAnimal,
                                                   columnPositionOfAnimal,
                                                   listOfPositionsCanCatchEnemy);
            }
        }

        private PositionOnField.PositionOnField CanEatEnemy(Animal[,] nextGenArray, List<PositionOnField.PositionOnField> listOfPositionsCanCatchEnemy)
        {
            foreach (var position in listOfPositionsCanCatchEnemy)
            {
                if (nextGenArray[position.RowPosition, position.ColumnPosition] != null
                    && nextGenArray[position.RowPosition, position.ColumnPosition].Name == EnemiesName)
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
            List<PositionOnField.PositionOnField> freePositionsOnField,
            List<PositionOnField.PositionOnField> listOfPositionsCanCatchEnemy)
        {
            if (base.EnemyIsToTheRightOfAnimal(columnPositionOfAnimal, columnPositionOfEnemy))
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
            List<PositionOnField.PositionOnField> freePositionsOnField,
            List<PositionOnField.PositionOnField> listOfPositionsCanCatchEnemy)
        {
            if (base.EnemyIsLowerThanAnimal(rowPositionOfAnimal, rowPositionOfEnemy))
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

using Savannaah.Animals;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Savannah.FieldOfGame
{
    public class GameField
    {
        public Animal[,] GameState { get; set; }

        private Random random;

        public GameField()
        {
            GameState = new Animal[this.GetGameFieldSize(), this.GetGameFieldSize()];
            random = new Random();
        }

        public Animal[,] CreateNewGameState()
        {
            return new Animal[this.GetGameFieldSize(), this.GetGameFieldSize()];
        }

        public int GetGameFieldSize()
        {
            return 10;
        }

        public PositionOnField.PositionOnField GetRandomAndFreePositionOnField()
        {
            var freePositionList = this.GetAllFreePositionsOnField();
            var randomPositionNumberFromTheList = random.Next(0, freePositionList.Count);
            return freePositionList.ElementAt(randomPositionNumberFromTheList);
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

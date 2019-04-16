using Savannaah.Animals;
using Savannah.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Savannah.FieldOfGame
{
    public class GameField : IGameField
    {
        public Animal[,] GameState { get; set; }

        private Random random;

        private readonly IConfiguration configuration;

        public GameField(IConfiguration configuration)
        {
            this.configuration = configuration;
            GameState = new Animal[GetGameFieldSize(), GetGameFieldSize()];
            random = new Random();
        }

        public Animal[,] CreateNewGameState()
        {
            return new Animal[GetGameFieldSize(), GetGameFieldSize()];
        }

        public int GetGameFieldSize()
        {
            return configuration.GetGameFieldSize();
        }

        public PositionOnField.PositionOnField GetRandomAndFreePositionOnField()
        {
            var freePositionList = GetAllFreePositionsOnField();
            if (freePositionList.Count > 0)
            {
                var randomPositionNumberFromTheList = random.Next(0, freePositionList.Count);
                return freePositionList.ElementAt(randomPositionNumberFromTheList);
            }
            else
            {
                return null;
            }
        }

        public List<PositionOnField.PositionOnField> GetAllFreePositionsOnField()
        {
            var freePositionList = new List<PositionOnField.PositionOnField>();

            for (int row = 0; row < GetGameFieldSize(); row++)
            {
                for (int column = 0; column < GetGameFieldSize(); column++)
                {
                    if (CheckIfThisPositionIsFree(GameState, row, column))
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

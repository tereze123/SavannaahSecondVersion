using Savannaah.Animals;
using Savannah.Common;
using Savannah.Common.Facades;
using Savannah.Common.Factories;
using Savannah.PositionOnField.Factories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Savannah.FieldOfGame
{
    public class GameField : IGameField
    {
        public Animal[,] GameState { get; set; }


        private RandomiserFacade random;
        private readonly IConfiguration configuration;
        private readonly IPositionOnFieldFactory positionOnFieldFactory;
        private readonly IRandomiserFactory randomiserFactory;

        public GameField(IConfiguration configuration, IPositionOnFieldFactory positionOnFieldFactory, IRandomiserFactory randomiserFactory)
        {
            this.configuration = configuration;
            this.positionOnFieldFactory = positionOnFieldFactory;
            this.randomiserFactory = randomiserFactory;
            GameState = CreateNewGameState();
            random = randomiserFactory.GetRandom();
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
            var freePositionList = positionOnFieldFactory.GetNewListOfPositionsOnField();

            for (int row = 0; row < GetGameFieldSize(); row++)
            {
                for (int column = 0; column < GetGameFieldSize(); column++)
                {
                    if (CheckIfThisPositionIsFree(GameState, row, column))
                    {
                        freePositionList.Add(positionOnFieldFactory.GetNewPositionOnField(row, column));
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

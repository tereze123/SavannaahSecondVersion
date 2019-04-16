using Savannaah.Animals;
using Savannah.Animals;
using Savannah.Common;
using Savannah.FieldOfGame;

namespace Savannah.GameCoordinator.Loop
{
    public class LoopOfGame : ILoopOfGame
    {
        private readonly FieldOfGame.IGameField gameField;
        private readonly IConfiguration configuration;

        public LoopOfGame(FieldOfGame.IGameField gameField, IConfiguration configuration)
        {
            this.gameField = gameField;
            this.configuration = configuration;
        }

        public void LoopThroughTheGame()
        {
            var nextGenerationArray = gameField.CreateNewGameState();

            for (int row = 0; row < gameField.GetGameFieldSize(); row++)
            {
                for (int column = 0; column < gameField.GetGameFieldSize(); column++)
                {
                    if (gameField.GameState[row, column] != null)
                    {
                        Move(nextGenerationArray, row, column);
                    }
                }
            }
            gameField.GameState = nextGenerationArray;
        }

        private void Move(Animal[,] nextGenerationArray, int row, int column)
        {
            var positionOfEnemy = new PositionOnField.PositionOnField();
            positionOfEnemy = gameField.GameState[row, column].EnemysPositionOnField(gameField.GameState, row, column);

            if (positionOfEnemy.IsEnemyInViewRange)
            {
                gameField.GameState[row, column].EnemyIsInRangeMovementNextPosition(
                    gameField.GameState,
                    nextGenerationArray,
                    positionOfEnemy.RowPosition,
                    positionOfEnemy.ColumnPosition,
                    row,
                    column);
            }
            else
            {
                gameField.GameState[row, column].PeaceStateMovementNextPosition(gameField.GameState,
                    nextGenerationArray, row, column);
            }
        }

        public void UsersTurnToAddAnimals(IGameField gameField, string userKeyPressed)
        {
            if (userKeyPressed == configuration.GetNameOfAntelope())
            {
                Animal antilope = new Antelope();
                PositionOnField.PositionOnField randomAndFreePosOnField = gameField.GetRandomAndFreePositionOnField();
                if (randomAndFreePosOnField != null)
                {
                    gameField.GameState[randomAndFreePosOnField.RowPosition, randomAndFreePosOnField.ColumnPosition] = antilope;
                }
            }
            else if (userKeyPressed == configuration.GetNameOfLion())
            {
                Animal lion = new Lion();
                PositionOnField.PositionOnField randomAndFreePosOnField = gameField.GetRandomAndFreePositionOnField();
                if (randomAndFreePosOnField != null)
                {
                    gameField.GameState[randomAndFreePosOnField.RowPosition, randomAndFreePosOnField.ColumnPosition] = lion;
                }
            }
        }
    }
}

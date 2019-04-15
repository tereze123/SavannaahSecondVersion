using Savannaah.Animals;
using Savannah.Animals;
using Savannah.FieldOfGame;

namespace Savannah.GameCoordinator.Loop
{
    public class LoopOfGame
    {
        private readonly FieldOfGame.GameField gameField;

        public LoopOfGame(FieldOfGame.GameField gameField)
        {
            this.gameField = gameField;
        }

        public void LoopThroughTheGame()
        {
            var nextGenerationArray = gameField.CreateNewGameState();

            for (int row = 0; row < gameField.GetGameFieldSize(); row++)
            {
                for (int column = 0; column < gameField.GetGameFieldSize(); column++)
                {
                    if (gameField.GameState[row,column] != null)
                    {
                        gameField.GameState[row, column].PeaceStateMovementNextPosition(gameField.GameState,
                            nextGenerationArray, row, column);
                    }
                }
            }
            gameField.GameState = nextGenerationArray;
        }

        internal void UsersTurnToAddAnimals(GameField gameField, string userKeyPressed)
        {
            if (userKeyPressed == "A")
            {
                Animal antilope = new Antelope();
                PositionOnField.PositionOnField randomAndFreePosOnField =  gameField.GetRandomAndFreePositionOnField();
                gameField.GameState[randomAndFreePosOnField.RowPosition, randomAndFreePosOnField.ColumnPosition] = antilope;
            }
        }
    }
}

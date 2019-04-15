using Savannah.Common;
using Savannah.GameCoordinator.Loop;
using Savannah.InputAndOutput;
using System.Threading;

namespace Savannah.GameCoordinator
{
    public class GameManager
    {
        private readonly LoopOfGame loopOfGame;
        private readonly UserInput userInput;
        private readonly GameFieldDrawer gameFieldDrawer;
        private FieldOfGame.GameField gameField;
        private Configuration configuration;

        public GameManager(UserInput userInput, GameFieldDrawer gameFieldDrawer)
        {
            this.gameField = new FieldOfGame.GameField();
            this.loopOfGame = new LoopOfGame(gameField);
            this.userInput = userInput;
            this.gameFieldDrawer = gameFieldDrawer;
            configuration = new Configuration();
        }

        public void Start()
        {
            string userKeyPressed = string.Empty;
            gameFieldDrawer.DrawGameField(gameField);
            do
            {
                if (userInput.IsKeyPressed())
                {
                    userKeyPressed = userInput.ReturnKeyPressed();

                    if (userKeyPressed == configuration.GetNameOfAntelope() || userKeyPressed == configuration.GetNameOfLion())
                    {
                        loopOfGame.UsersTurnToAddAnimals(gameField, userKeyPressed);
                        gameFieldDrawer.DrawGameField(gameField);
                    }
                }
                loopOfGame.LoopThroughTheGame();
                Thread.Sleep(100);
                gameFieldDrawer.DrawGameField(gameField);
            } while (userKeyPressed != "ESC");
        }

    }
}

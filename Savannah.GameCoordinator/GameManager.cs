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

        public GameManager(UserInput userInput, GameFieldDrawer gameFieldDrawer)
        {
            this.gameField = new FieldOfGame.GameField();
            this.loopOfGame = new LoopOfGame(gameField);
            this.userInput = userInput;
            this.gameFieldDrawer = gameFieldDrawer;
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

                    if (userKeyPressed == "A")
                    {
                        loopOfGame.UsersTurnToAddAnimals(gameField, userKeyPressed);
                        gameFieldDrawer.DrawGameField(gameField);
                        Thread.Sleep(1000);
                    }
                }
                loopOfGame.LoopThroughTheGame();
                Thread.Sleep(1000);
                gameFieldDrawer.DrawGameField(gameField);
                Thread.Sleep(1000);
            } while (userKeyPressed != "ESC");
        }

    }
}

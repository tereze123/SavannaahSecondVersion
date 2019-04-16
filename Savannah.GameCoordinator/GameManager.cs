using Savannah.Common;
using Savannah.GameCoordinator.Loop;
using Savannah.InputAndOutput;
using System.Threading;

namespace Savannah.GameCoordinator
{
    public class GameManager
    {
        private readonly ILoopOfGame loopOfGame;
        private readonly IUserInput userInput;
        private readonly IGameFieldDrawer gameFieldDrawer;
        private FieldOfGame.IGameField gameField;
        private IConfiguration configuration;

        public GameManager(IUserInput userInput, 
                           IGameFieldDrawer gameFieldDrawer, 
                           IConfiguration configuration)
        {
            this.configuration = configuration;
            this.gameField = new FieldOfGame.GameField();
            this.loopOfGame = new LoopOfGame(gameField, configuration);
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

                    if (userKeyPressed == configuration.GetNameOfAntelope() || userKeyPressed == configuration.GetNameOfLion())
                    {
                        loopOfGame.UsersTurnToAddAnimals(gameField, userKeyPressed);
                        gameFieldDrawer.DrawGameField(gameField);
                    }
                }
                loopOfGame.LoopThroughTheGame();
                Thread.Sleep(500);
                gameFieldDrawer.DrawGameField(gameField);
            } while (userKeyPressed != "ESC");
        }

    }
}

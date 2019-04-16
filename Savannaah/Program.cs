using Savannah.Animals.Factories;
using Savannah.Common;
using Savannah.FieldOfGame;
using Savannah.FieldOfGame.Factories;
using Savannah.GameCoordinator;
using Savannah.GameCoordinator.Factories;
using Savannah.InputAndOutput;
using Savannah.PositionOnField;

namespace Savannaah
{
    class Program
    {
        static void Main(string[] args)
        {
            IConfiguration configuration = new Configuration();
            IUserInput userInput = new UserInputForConsole(configuration);
            IGameFieldDrawer gameFieldDrawer = new GameFieldDrawerForConsole();
            IPositionOnFieldValidation positionOnFieldValidation = new PositionOnFieldValidation(configuration);
            IAnimalFactory animalFactory = new AnimalFactory(configuration, positionOnFieldValidation);
            IGameFieldFactory gameFieldFactory = new GameFieldFactory(configuration);
            IGameField gameField = gameFieldFactory.GetGameField();
            ILoopOfGameFactory loopOfGameFactory = new LoopOfGameFactory(configuration, animalFactory);

            GameManager gameManager = new GameManager(userInput, gameFieldDrawer, configuration, animalFactory, gameFieldFactory, loopOfGameFactory);
            gameManager.Start();
        }
    }
}

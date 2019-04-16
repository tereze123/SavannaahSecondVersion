using Savannah.Animals.Factories;
using Savannah.Common;
using Savannah.GameCoordinator;
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

            GameManager gameManager = new GameManager(userInput, gameFieldDrawer, configuration, animalFactory);
            gameManager.Start();
        }
    }
}

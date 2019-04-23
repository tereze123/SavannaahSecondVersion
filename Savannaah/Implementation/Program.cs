using Savannah.Animals.Factories;
using Savannah.Client;
using Savannah.Common;
using Savannah.Common.Factories;
using Savannah.FieldOfGame;
using Savannah.FieldOfGame.Factories;
using Savannah.GameCoordinator;
using Savannah.GameCoordinator.Factories;
using Savannah.InputAndOutput;
using Savannah.PositionOnField;
using Savannah.PositionOnField.Factories;

namespace Savannaah
{
    class Program
    {
        static void Main(string[] args)
        {
            IConfiguration configuration = new Configuration();
            IRandomiserFactory randomiserFactory = new RandomiserFactory();
            IUserInput userInput = new UserInputForConsole(configuration);
            IPositionOnFieldFactory positionOnFieldFactory = new PositionOnFieldFactory();
            IGameFieldDrawer gameFieldDrawer = new GameFieldDrawerForConsole();
            IPositionOnFieldValidation positionOnFieldValidation = new PositionOnFieldValidation(configuration);
            IAnimalFactory animalFactory = new AnimalFactory(configuration, positionOnFieldValidation, positionOnFieldFactory, randomiserFactory);
            IGameFieldFactory gameFieldFactory = new GameFieldFactory(configuration, positionOnFieldFactory, randomiserFactory);
            IGameField gameField = gameFieldFactory.GetGameField();
            ILoopOfGameFactory loopOfGameFactory = new LoopOfGameFactory(configuration, animalFactory, positionOnFieldFactory);

            PluginLoader pluginLoader = new PluginLoader();
            pluginLoader.LoadPlugins(animalFactory);

            GameManager gameManager = new GameManager(userInput, gameFieldDrawer, configuration, animalFactory, gameFieldFactory, loopOfGameFactory);
            gameManager.Start();
        }


    }
}

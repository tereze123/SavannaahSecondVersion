using Savannah.Common;
using Savannah.GameCoordinator;
using Savannah.InputAndOutput;
using System;

namespace Savannaah
{
    class Program
    {
        static void Main(string[] args)
        {
            IConfiguration configuration = new Configuration();
            IUserInput userInput = new UserInputForConsole(configuration);
            IGameFieldDrawer gameFieldDrawer = new GameFieldDrawerForConsole();

            GameManager gameManager = new GameManager(userInput, gameFieldDrawer, configuration);
            gameManager.Start();
        }
    }
}

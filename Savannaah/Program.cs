using Savannah.GameCoordinator;
using Savannah.InputAndOutput;
using System;

namespace Savannaah
{
    class Program
    {
        static void Main(string[] args)
        {
            UserInput userInput = new UserInput();
            GameFieldDrawer gameFieldDrawer = new GameFieldDrawer();

            GameManager gameManager = new GameManager(userInput, gameFieldDrawer);
            gameManager.Start();
        }
    }
}

using Savannah.Common;
using System;
using System.ComponentModel.Composition;

namespace Savannah.InputAndOutput
{
    public class UserInputForConsole : IUserInput
    {
        private readonly IConfiguration configuration;

        [ImportingConstructor]
        public UserInputForConsole(IConfiguration configuration)
        {
            this.configuration = configuration;
 
        }

        public string ReturnKeyPressed()
        {
            ConsoleKeyInfo consoleKeyInfo;
            do
            {
                consoleKeyInfo = Console.ReadKey(true);
                if (consoleKeyInfo.Key == ConsoleKey.A) { return configuration.GetNameOfAntelope(); }
                else if (consoleKeyInfo.Key == ConsoleKey.L) { return configuration.GetNameOfLion(); }
            } while (consoleKeyInfo.Key != ConsoleKey.Escape);
            return "ESC";
        }

        public bool IsKeyPressed()
        {
            if (Console.KeyAvailable)
            {

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

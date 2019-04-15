using Savannah.Common;
using System;

namespace Savannah.InputAndOutput
{
    public class UserInput
    {
        private Configuration configuration;
        public UserInput()
        {
            configuration = new Configuration();
        }
        private ConsoleKeyInfo consoleKeyInfo;

        public string ReturnKeyPressed()
        {
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

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
                return consoleKeyInfo.Key.ToString();
            } while (consoleKeyInfo.Key != ConsoleKey.Escape);
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

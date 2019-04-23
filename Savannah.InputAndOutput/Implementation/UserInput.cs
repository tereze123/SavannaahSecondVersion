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
                consoleKeyInfo = Console.ReadKey(true);
                return consoleKeyInfo.Key.ToString();
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

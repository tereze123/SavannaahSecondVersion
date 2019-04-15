﻿using System;

namespace Savannah.InputAndOutput
{
    public class UserInput
    {
        private ConsoleKeyInfo consoleKeyInfo;

        public string ReturnKeyPressed()
        {
            do
            {
                consoleKeyInfo = Console.ReadKey(true);
                if (consoleKeyInfo.Key == ConsoleKey.A) { return "A"; }
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

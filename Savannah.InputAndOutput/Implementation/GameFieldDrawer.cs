using Savannah.FieldOfGame;
using System;
using System.ComponentModel.Composition;

namespace Savannah.InputAndOutput
{
    public class GameFieldDrawerForConsole : IGameFieldDrawer
    {
        public void DrawGameField(IGameField gameField)
        {
            Console.CursorVisible = false;

            for (int rowNumber = 0; rowNumber < gameField.GameState.GetLength(0); rowNumber++)
            {
                for (int columnNumber = 0; columnNumber < gameField.GameState.GetLength(0); columnNumber++)
                {
                    Console.SetCursorPosition(columnNumber,+ rowNumber);
                    this.OutputAnimalNameOrBlank(gameField, rowNumber, columnNumber);
                }
                Console.WriteLine();
            }
        }

        private void OutputAnimalNameOrBlank(IGameField gameField, int rowNumber, int columnNumber)
        {
            if (gameField.GameState[rowNumber, columnNumber] == null)
            {
                Console.Write(" ");
            }
            else
            {
                Console.Write(gameField.GameState[rowNumber, columnNumber].Name);
            }
        }
    }
}

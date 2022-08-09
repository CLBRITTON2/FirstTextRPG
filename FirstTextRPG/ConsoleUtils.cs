using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace FirstTextRPG
{
    public static class ConsoleUtils
    {
        #region Method: Output type out lines effect
        public static void TypeLine(ConsoleColor color, string typeLine) // Method to create "typing" visual effect onto console for story line
        {
            Console.ForegroundColor = color;

            for (int i = 0; i < typeLine.Length; i++) // Puts a 40ms pause after each character in the line is typed
            {
                PrintLogic(typeLine[i].ToString());
                Thread.Sleep(10);
            }

            Console.ResetColor();
        }
        #endregion

        #region Method: Output with colored text
        public static void Print(ConsoleColor color, string text, string end = "\n")
        {
            Console.ForegroundColor = color;
            PrintLogic(text, end);
            Console.ResetColor();
        }
        #endregion

        #region Method: Output default color text
        public static void Print(string text, string end = "")
        {
            PrintLogic(text, end);
        }
        #endregion

        #region Method: Array printer w/ 2 colors
        public static void ArrayPrinter(string[] dialogueBlock, ConsoleColor color1, ConsoleColor color2)
        {
            int x = Console.CursorLeft;
            int y = Console.CursorTop;
            for (int i = 0; i < dialogueBlock.Length; i++)
            {
                Console.SetCursorPosition(x, y + i);
                if (dialogueBlock[i] == dialogueBlock.ElementAt(0))
                {
                    ConsoleUtils.Print(color1, dialogueBlock[i], "");
                }
                else if (dialogueBlock[i] == dialogueBlock.ElementAt(1))
                {
                    ConsoleUtils.Print(color2, dialogueBlock[i], "");
                }

                else
                {
                    ConsoleUtils.Print(dialogueBlock[i]);
                }
            }
        }
        #endregion

        #region Method: Array printer w/ TypeLine and yellow text
        public static void ArrayPrinter(string[] dialogueBlock)
        {
            int x = Console.CursorLeft = 0;
            int y = Console.CursorTop;
            for (int i = 0; i < dialogueBlock.Length; i++)
            {
                ConsoleUtils.TypeLine(ConsoleColor.Yellow, dialogueBlock[i]);
            }
        }
        #endregion

        #region Method: Print logic with line break
        private static void PrintLogic(string input, string end = "")
        {
            Console.Write(input + end);
        }
        #endregion

        #region Method: Continue
        public static void Continue()
        {
            Print(ConsoleColor.Yellow, "\n(Press any key to continue)");
            Console.ReadKey(true);
        }
        #endregion

        #region Method: End game
        public static void EndGame()
        {
            PrintLogic("\n(Press any key to exit)");
            Console.ReadKey(true);
            Environment.Exit(0);
        }
        #endregion
    }
}

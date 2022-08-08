using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        #region Method: Output will just change text color
        public static void Print(ConsoleColor color, string text, string end = "\n")
        {
            Console.ForegroundColor = color;
            PrintLogic(text, end);
            Console.ResetColor();
        }
        #endregion

        #region Method: Print default color text
        public static void Print(string text, string end = "")
        {
            PrintLogic(text, end);
        }
        #endregion

        #region Method: Print logic with line break
        private static void PrintLogic(string input, string end = "")
        {
            Console.Write(input + end);
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

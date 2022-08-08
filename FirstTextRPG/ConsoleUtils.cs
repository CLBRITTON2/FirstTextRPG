using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstTextRPG
{
    static class ConsoleUtils
    {
        #region Method: Type out lines effect
        public static void TypeLine(ConsoleColor color, string line) // Method to create "typing" visual effect onto console for story line
        {
            Console.ForegroundColor = color;

            for (int i = 0; i < line.Length; i++) // Puts a 40ms pause after each character in the line is typed
            {
                Console.Write(line[i]);
                Thread.Sleep(30);
            }

            Console.ResetColor();
        }
        #endregion
        #region Method: Change text color
        public static void ChangeColor(ConsoleColor color, string text)
        {
            Console.ForegroundColor = color;

            Console.WriteLine(text);

            Console.ResetColor();
        }
        #endregion
        #region Method: End game
        public static void EndGame()
        {
            Console.WriteLine("\n(Press any key to exit)");
            Console.ReadKey(true);
            Environment.Exit(0);
        } 
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstTextRPG.Characters
{
    internal class Enemy : Character
    {
        #region Enemy backing field
        private string AsciiArt; // Enemy backing field
        private ConsoleColor Color; // Enemy backing field 
        public int ExpValue { get; protected set; } // Automatic properties, exp value can only be set by Enemy (no derived classes)
        #endregion

        #region Enemy constructor
        public Enemy(string name, int hitPoints, int level, bool isAlive, ConsoleColor color, string asciiArt, int expValue) // Enemy constructor
    : base(name, hitPoints, level, isAlive) // Passing Enemy values to the Character (base class) constructor
        {
            AsciiArt = asciiArt;
            Color = color;
            ExpValue = expValue;
        }
        #endregion

        #region Method: Display enemy info
        public override void DisplayInfo() // Inherit base class display info, add in what makes an enemy unique.
        {
            Console.ForegroundColor = Color; // Enemy color has the ability to change
            ConsoleUtils.Print($"{AsciiArt}\n"); // Enemy Ascii art
            base.DisplayInfo();
            Console.ResetColor();
        }
        #endregion

        #region Method: Enemy combat system
        public void Battle(Character aPlayer)
        {
            DealDamage(aPlayer);
        }
        #endregion
    }
}

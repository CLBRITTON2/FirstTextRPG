using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstTextRPG
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
            Console.WriteLine($"{AsciiArt}\n"); // Enemy Ascii art
            base.DisplayInfo();
            Console.ResetColor();
        }
        #endregion

        #region Method: Enemy combat system
        public void Battle(Character aPlayer)
        {
            HitPercent = NumberGenerator.Next(1, 100); // Creating rng for missing an attack
            HitValue = NumberGenerator.Next(1, 5); // Setting a HitValue for enemy

            if (HitPercent <= 85) // Has an 85% chance to deal damage
            {
                ConsoleUtils.ChangeColor(ConsoleColor.Red, $"\n{Name} hits {aPlayer.Name} for {HitValue} hitpoints!");
                aPlayer.TakeDamage(HitValue);
            }
            else// There is a 15% chance the enemy will miss completely dealing 0 damage
            {
                ConsoleUtils.ChangeColor(ConsoleColor.Red, $"\n{Name} misses {aPlayer.Name} and deals 0 damage");
            }
        }
        #endregion
    }
}

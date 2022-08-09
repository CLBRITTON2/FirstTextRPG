using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstTextRPG.Characters
{
    internal class Character
    {
        #region Character backing fields
        public string Name { get; protected set; } // Automatic properties for Name. Any class can read Name, but only the base/derived clases can set Name.
        public double HitPoints { get; protected set; } // Automatic properties for HitPoints. Any class can read HitPoints, but only the base/derived clases can set HitPoints.
        public double MaxHitPoints { get; protected set; } // Automatic properties for MaxHitPoints. Any class can read MaxHitPoints, but only the base/derived clases can set MaxHitPoints.
        public int HitValue { get; protected set; } // Automatic properties for HitValue. Any class can read HitValue, but only the base/derived clases can set HitValue.
        public int HitPercent { get; protected set; } // Automatic properties for HitPercent. Any class can read HitPercent, but only the base/derived clases can set HitPercent.
        public int Level { get; protected set; } // Automatic properties for Level. Any class can read Level, but only the base/derived clases can set Level.
        public bool IsAlive { get; protected set; } // Automatic properties for IsAlive. Any class can read IsAlive, but only the base/derived clases can set IsAlive.
        public bool IsDead { get => HitPoints <= 0; } // Any time IsDead is used it will evaluate and return result
        public Random NumberGenerator { get; protected set; } // Field to store a random number generator
        #endregion

        #region Character constructor
        public Character(string name, double hitPoints, int level, bool isAlive) // Character Constructor
        {
            Name = name;
            HitPoints = hitPoints;
            MaxHitPoints = hitPoints; // Assuming that the player will be instanciated with max hitpoints
            Level = level;
            IsAlive = isAlive;
            NumberGenerator = new Random(); // Instantiate random number generator
        }
        #endregion

        #region Method: Display char info
        public virtual void DisplayInfo()
        {
            ConsoleUtils.Print($"\n{Name}", "\n");
            ConsoleUtils.Print($"Level: {Level}", "\n");
            ConsoleUtils.Print($"Hitpoints: {HitPoints}", "\n");
            DisplayHitpointsBar();
            ConsoleUtils.Print($"\n__________________", "\n");
        }
        #endregion

        #region Method: HP bar and functionality
        public void DisplayHitpointsBar()
        {
            ConsoleUtils.Print(""); // Spacer to keep hitpoints bar in bounds

            Console.BackgroundColor = ConsoleColor.Green;
            for (int i = 0; i < HitPoints; i++) // For every hitpoint, write out 1 green space
            {
                ConsoleUtils.Print(" ");
            }

            Console.BackgroundColor = ConsoleColor.Red;
            for (double i = HitPoints; i < MaxHitPoints; i++) // For every missing hitpoint, write out 1 red space
            {
                ConsoleUtils.Print(" ");
            }
            Console.ResetColor();
            ConsoleUtils.Print($"({HitPoints}/{MaxHitPoints})"); // Display hp/Max hp beside hp bar
        }
        #endregion

        #region Method: Taking damage
        // TODO: return a damage value
        public void TakeDamage(int hitValue) // Damage is based off a random number generator
        {
            HitValue = hitValue;

            HitPoints -= hitValue;

            if (HitPoints < 0) // If statement to ensure that Health value never goes below 0
            {
                HitPoints = 0;
            }

        }
        #endregion

        #region Method: Deal damage
        // TODO return a damage value
        public void DealDamage(Character character)
        {
            HitPercent = NumberGenerator.Next(1, 100); // Creating rng for missing an attack
            HitValue = NumberGenerator.Next(1, 5); // Setting a HitValue for enemy

            if (HitPercent <= 85) // Has an 85% chance to deal damage
            {
                ConsoleUtils.Print($"\n{Name} hits {character.Name} and deals {HitValue} damage", "\n");
                character.TakeDamage(HitValue);
            }
            else// There is a 15% chance the attack will miss completely dealing 0 damage
            {
                ConsoleUtils.Print($"\n{Name} misses and deals 0 damage", "\n");
            }
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstTextRPG
{
    internal class Player : Character
    {
        #region Backing fields
        public double TotalExperience { get; set; } // Automatic properties
        public double ExperienceToNextLevel { get; protected set; } = 100; // Automatic properties, can only be set in this class
        #endregion

        #region Player constructor
        public Player(string name, int hitPoints, int level, bool isAlive)
    : base(name, hitPoints, level, isAlive) // passing Player values to the Character (base class) constructor
        {

        }
        #endregion

        #region Method: Display player info
        public override void DisplayInfo() // Inherit base class display info, add in what makes a player unique.
        {
            Console.ForegroundColor = ConsoleColor.Green; // Player color will always be green
            base.DisplayInfo();
            Console.ResetColor();
        }
        #endregion

        #region Method: Player combat system
        public void Battle(Character anEnemy)
        {
            ConsoleUtils.Print(ConsoleColor.Yellow, "\n(Select an option from the menu to continue)");
            ConsoleUtils.Print(ConsoleColor.Yellow, "1) Attack \n2) Attempt special attack \n3) Defend \n4) Heal");
            ConsoleUtils.Print($"__________________", "\n");

            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            Thread.Sleep(1000);

            switch (keyInfo.Key)
            {
                case ConsoleKey.D1:
                    SingleAttack(anEnemy);
                    break;

                case ConsoleKey.D2:
                    SpecialAttack(anEnemy);
                    break;

                case ConsoleKey.D3:
                    Defend(true); //Turns "isDefending" bool to true, so player will be defending and will execute code in that method
                    break;

                case ConsoleKey.D4:
                    Heal();
                    break;

                default:
                    ConsoleUtils.Print(ConsoleColor.DarkRed, "Invalid entry. Please choose a number 1-4");
                    Battle(anEnemy); //Sends player back to attack menu to select a valid option
                    break;
            }

        }
        #endregion

        #region Method: Player heal
        private void Heal()
        {
            int healthPlus = NumberGenerator.Next(1, 8);
            if (HitPoints == MaxHitPoints)
            {
                ConsoleUtils.Print(ConsoleColor.DarkRed, $"\nYour hitpoints cannot exceed {MaxHitPoints}"); // Ensures the player doesn't heal over max HP
            }
            else
            {
                HitPoints += healthPlus;
                ConsoleUtils.Print(ConsoleColor.Green, $"\n{Name} has healed for {healthPlus} hitpoints.");
            }

        }
        #endregion

        #region Method: Player defends
        private void Defend(bool isDefending)
        {
            HitValue = 0; // Enemy will hit 0 by default if player is defending
            ConsoleUtils.Print(ConsoleColor.Blue, $"\n{Name} defends against the enemy attack and recieves no damage.");
        }
        #endregion

        #region Method: Single attack
        private void SingleAttack(Character anEnemy)
        {
            DealDamage(anEnemy);
        }
        #endregion

        #region Method: Special attack
        private void SpecialAttack(Character anEnemy)
        {
            if (HitPercent <= 50) // Has a 50% chance to deal double damage
            {
                ConsoleUtils.Print($"\n{Name} uses a special attack hitting {anEnemy.Name} for {HitValue * 2} hitpoints!", "\n");
                anEnemy.TakeDamage(HitValue * 2);
            }
            else // There is a 50% chance the player will miss completely dealing 0 damage
            {
                ConsoleUtils.Print($"\n{Name} misses {anEnemy.Name} and deals 0 damage", "\n");
            }
        }
        #endregion

        #region Method: Player level up
        public void LevelUp()
        {
            while (TotalExperience >= ExperienceToNextLevel) // While the player has enough total exp to level up
            {
                Console.Clear();
                MaxHitPoints += 2;
                HitValue += 2;
                Level++;
                ConsoleUtils.Print(ConsoleColor.Green, $"Congratulations, you've leveled up! You're now level {Level}");
                Thread.Sleep(1000);
                ConsoleUtils.Print(ConsoleColor.Green, "Max HP + 2, Max hit + 2, Level + 1"); // Prints above changes to the player
                Thread.Sleep(1000);
                TotalExperience -= ExperienceToNextLevel;
                ExperienceToNextLevel *= 1.7; // Exp starts at 100 and multiplies 1.7x every time player levels up
                ConsoleUtils.Print(ConsoleColor.Green, $"Experience to next level: {ExperienceToNextLevel}");
                Thread.Sleep(1000);
            }
        }
        #endregion
    }
}

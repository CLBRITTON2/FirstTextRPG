using FirstTextRPG.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstTextRPG
{
    internal class Game
    {
        #region Class level variables (not smart long term)
        // Class level variables will maike it hard to track down problems in the future
        private List<Enemy> Monkeys; // List of enemy monkeys
        private Player Player;
        private Enemy TheMonkey; // The Index monkey
        private Enemy SpiderMonkey; // Enemy monkey
        private Enemy Baboon; // Enemy monkey
        private Enemy Mandrill; // Enemy monkey
        private Enemy HowlerMonkey; // Enemy monkey
        private Enemy Gorilla; // Enemy monkey
        #endregion

        #region Game constructor instansiating enemies and enemy list
        public Game() // Instansiating all enemies and enemy list accessed by multiple methods
        {
            SpiderMonkey = new Enemy("Spider Monkey", 10, 1, true, ConsoleColor.Red, AsciiArt.spiderMonkey, 100);
            Baboon = new Enemy("Baboon", 15, 2, true, ConsoleColor.Red, AsciiArt.baboon, 200);
            Mandrill = new Enemy("Mandrill", 20, 3, true, ConsoleColor.Red, AsciiArt.mandrill, 300);
            HowlerMonkey = new Enemy("Howler Monkey", 25, 4, true, ConsoleColor.Red, AsciiArt.howler, 400);
            Gorilla = new Enemy("Gorilla", 30, 5, true, ConsoleColor.Red, AsciiArt.gorilla, 500);
            Monkeys = new List<Enemy>() { SpiderMonkey, Baboon, Mandrill, HowlerMonkey, Gorilla }; // Creating the list of monkeys to fight
        }
        #endregion

        #region Method: Main menu
        public void MainMenu()
        {
            ConsoleUtils.Print(AsciiArt.welcome);
            ConsoleUtils.Print("Seems like a nice day to take a trip to the zoo...", "\n");
            string[] mainOptions = {"(Press 1 to enter the zoo)", "(Press 2 to exit)\n" };
            ConsoleUtils.ArrayPrinter(mainOptions, ConsoleColor.Green, ConsoleColor.Red); 

            ConsoleKeyInfo keyChoice = Console.ReadKey(true);
            if (keyChoice.Key == ConsoleKey.D1)
            {
                Console.Clear();
                Intro(); // Sends the user into the intro if they press 1
            }
            else
            {
                ConsoleUtils.EndGame(); // Ends game if 2
            }
        }
        #endregion

        #region Method: Game introduction
        public void Intro()
        {
            string[] introDialogue = { "Greetings adventurer...", "\nPlease enter your name: " };
            ConsoleUtils.ArrayPrinter(introDialogue);

            string playerName = Console.ReadLine().Trim(); // Trim helps readability for username as the game calls the username
            Player = new Player(playerName, 20, 1, true); // Instanciates a new player

            SituationPrompt(playerName);
        }
        #endregion

        #region Method: Situation prompt
        public void SituationPrompt(string playerName)
        {
            Console.Clear();
            string[] situationDialogue = 
            {
               $"Zoo keeper: {playerName}, we need your help!",
               "\nZoo keeper: There was a radioactive spill that contaminated the monkey exhibit at our local zoo...",
               "\nZoo keeper: The mutated monkeys have gone on a rampage and we need someone to help us stop them!",
               "\nZoo keeper: Will you help us?"
            };
            ConsoleUtils.ArrayPrinter(situationDialogue);

            string[] situationOptions = { "\nType \"YES\" to help the Zoo keeper..", "\nType \"NO\" to exit...\n" };
            ConsoleUtils.ArrayPrinter(situationOptions, ConsoleColor.Green, ConsoleColor.Red);

            string userChoice = Console.ReadLine().ToUpper();

            if (userChoice == "YES")
            {
                Console.Clear();
                ConsoleUtils.TypeLine(ConsoleColor.Yellow, "Zoo keeper: Here comes your first opponent! Get ready! \n");
                ConsoleUtils.Continue();
                Console.Clear();
                GameLoop(); // If user chooses to help zoo keeper.. They enter the game loop.
            }
            else // Add exception handling possibly
            {
                ConsoleUtils.EndGame();
            }
        }
        #endregion

        #region Method: Game loop
        public void GameLoop()
        {
            TheMonkey = Monkeys[0];

            for (int i = 0; i < Monkeys.Count; i += 1) // For loop to cycle through Monkeys list to call the next Monkey when a monkey dies
            {
                TheMonkey = Monkeys[i];
                BattleLoop();

                if (Player.IsDead)
                {
                    Console.Clear();
                    ConsoleUtils.Print(ConsoleColor.DarkRed, $"Oh dear! You're dead!");
                    ConsoleUtils.Continue();
                    ConsoleUtils.EndGame();
                }
                else if (TheMonkey.IsDead && i < 4) // Will cycle through victory progresion method until index hits 4
                {
                    Console.Clear();
                    ConsoleUtils.Print(ConsoleColor.DarkRed, $"{TheMonkey.Name} has died!");
                    ConsoleUtils.Continue();

                    VictoryProgression();
                }
                else
                {
                    WinTheGame(); // Calls win game method when player defeats all enemies in the list
                }
            }
        }
        #endregion

        #region Method: Individual battle loop
        private void BattleLoop()
        {
            while (TheMonkey.IsAlive && Player.IsAlive) // Will loop through combat while monkey and player are alive
            {
                Console.Clear();
                TheMonkey.DisplayInfo(); // Display current enemy monkey info/health
                Player.DisplayInfo(); // Displayer current player info/health               

                Player.Battle(TheMonkey); // Starts with player attacking monkey

                TheMonkey.Battle(Player);// Monkey attacks back
                ConsoleUtils.Continue();

                if (TheMonkey.IsDead || Player.IsDead) // Checks to see if player/enemy are dead after combat
                {
                    break; // If player or monkey are dead console will break out of loop
                }
            }
        }
        #endregion

        #region Method: Victory progression
        private void VictoryProgression() // In the case that a player kills a monkey / will happen every time
        {
            Console.Clear();
            ConsoleUtils.TypeLine(ConsoleColor.Yellow, $"Zoo keeper: Well done! You've defeated {TheMonkey.Name} and recieve {TheMonkey.ExpValue} exp!\n");

            double expGain = TheMonkey.ExpValue; // Sets monkey exp value to the exp that the player will gain for killing monkey
            Player.TotalExperience += expGain; // Tracks player's total exp
            ConsoleUtils.Continue();


            if (Player.TotalExperience >= Player.ExperienceToNextLevel) // Player will level up if they've met experience requirement
            {
                Player.LevelUp();
                NextFightPrompt();
            }
            else
            {
                NextFightPrompt(); // If player doesn't earn req exp they won't level up and will continue to the next fight
            }
        }
        #endregion

        #region Method: Next fight prompt
        private void NextFightPrompt()
        {
            Console.Clear();
            ConsoleUtils.TypeLine(ConsoleColor.Yellow, "Zoo keeper: Get ready for your next fight!\n");
            ConsoleUtils.Continue();
        }
        #endregion

        #region Method: Winner prompt
        private void WinTheGame()
        {
            Console.Clear();
            string[] winDialogue =
            {
                $"Zoo keeper: Great job {Player.Name}!",
                "\nZoo keeper: You managed to save the zoo by defeating all of the contaminated monkeys!",
                "\nZoo keeper: Thank you so much for your help!"
            };
            ConsoleUtils.ArrayPrinter(winDialogue);
            ConsoleUtils.EndGame();
        }
        #endregion
    }
}

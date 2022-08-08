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
            ConsoleUtils.Print(ConsoleColor.Green, "(Press 1 to enter the zoo)");
            ConsoleUtils.Print(ConsoleColor.Red, "(Press 2 to exit)");
            Console.WriteLine();

            ConsoleKeyInfo keyChoice = Console.ReadKey(true);
            if (keyChoice.Key == ConsoleKey.D1)
            {
                Console.Clear();
                Intro(); // Sends the user into the intro if they press 1
            }
            else
            {
                ConsoleUtils.EndGame();
            }
        }
        #endregion

        #region Method: Game introduction
        public void Intro()
        {
            ConsoleUtils.TypeLine(ConsoleColor.Yellow, "Greetings adventurer...");

            Thread.Sleep(100); // 1 second pause between welcome and enter username prompt

            ConsoleUtils.TypeLine(ConsoleColor.Yellow, "\nPlease enter your name: ");
            string playerName = Console.ReadLine().Trim(); // Trim helps readability for username as the game calls the username
            Player = new Player(playerName, 20, 1, true); // Instanciates a new player

            SituationPrompt(playerName);
        }
        #endregion

        #region Method: Situation prompt
        public void SituationPrompt(string playerName)
        {
            Console.Clear();
            Thread.Sleep(1500);
            ConsoleUtils.TypeLine(ConsoleColor.Yellow, $"Zoo keeper: {playerName}, we need your help!");
            ConsoleUtils.TypeLine(ConsoleColor.Yellow, "\nZoo keeper: There was a radioactive spill that contaminated the monkey exhibit at our local zoo...");
            Thread.Sleep(1500);
            ConsoleUtils.TypeLine(ConsoleColor.Yellow, "\nZoo keeper: The mutated monkeys have gone on a rampage and we need someone to help us stop them!"); ;
            ConsoleUtils.TypeLine(ConsoleColor.Yellow, "\nZoo keeper: Will you help us?");
            Thread.Sleep(1500);
            ConsoleUtils.Print(ConsoleColor.Green, "\nType \"YES\" to help the Zoo keeper..");
            ConsoleUtils.Print(ConsoleColor.Red, "Type \"NO\" to exit...");

            string userChoice = Console.ReadLine().ToUpper();

            if (userChoice == "YES")
            {
                Console.Clear();

                ConsoleUtils.TypeLine(ConsoleColor.Yellow, "Zoo keeper: Here comes your first opponent! Get ready!");
                Thread.Sleep(1000);

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
                    ConsoleUtils.Print(ConsoleColor.DarkRed, $"Oh dear! You're dead!");
                    Thread.Sleep(1000);
                    ConsoleUtils.EndGame();
                }
                else if (TheMonkey.IsDead && i < 4) // Will cycle through victory progresion method until index hits 4
                {
                    ConsoleUtils.TypeLine(ConsoleColor.DarkRed, $"{TheMonkey.Name} has died!");

                    ConsoleUtils.Print(ConsoleColor.Yellow, "(Press any key to continue)");
                    Console.ReadKey(true);

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
                Thread.Sleep(1000);

                TheMonkey.Battle(Player);// Monkey attacks back
                Thread.Sleep(1000);

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
            ConsoleUtils.TypeLine(ConsoleColor.Yellow, $"Zoo keeper: Well done! You've defeated {TheMonkey.Name} and recieve {TheMonkey.ExpValue} exp!");

            double expGain = TheMonkey.ExpValue; // Sets monkey exp value to the exp that the player will gain for killing monkey
            Player.TotalExperience += expGain; // Tracks player's total exp
            Thread.Sleep(1000);


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
            ConsoleUtils.TypeLine(ConsoleColor.Yellow, "\nZoo keeper: Get ready for your next fight!");

            Thread.Sleep(1000);
            ConsoleUtils.Print(ConsoleColor.Yellow, "\n(Press any key to continue)"); // Will continue to next fight when a key is pressed
            Console.ReadKey(true);
        }
        #endregion

        #region Method: Winner prompt
        private void WinTheGame()
        {
            Console.Clear();
            ConsoleUtils.TypeLine(ConsoleColor.Yellow, $"Zoo keeper: Great job {Player.Name}!");
            ConsoleUtils.TypeLine(ConsoleColor.Yellow, $"\nZoo keeper: You managed to save the zoo by defeating all of the contaminated monkeys!");
            ConsoleUtils.TypeLine(ConsoleColor.Yellow, $"\nZoo keeper: Thank you so much for your help!");
            Thread.Sleep(1000);
            ConsoleUtils.EndGame();
        }
        #endregion
    }
}

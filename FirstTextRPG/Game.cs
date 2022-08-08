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
        private Player Player1; // Player 1
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
            Console.Title = "Monkey Mayhem"; // Set title
            Console.WriteLine(AsciiArt.welcome);
            Console.WriteLine("\nSeems like a nice day to take a trip to the zoo...");
            ChangeColor(ConsoleColor.Green, "\n(Press 1 to enter the zoo)");
            ChangeColor(ConsoleColor.Red, "\n(Press 2 to exit)");
            Console.WriteLine();

            ConsoleKeyInfo keyChoice = Console.ReadKey(true);
            if (keyChoice.Key == ConsoleKey.D1)
            {
                Console.Clear();
                Intro(); // Sends the user into the intro if they press 1
            }
            else
            {
                Console.Clear();
                Console.WriteLine("(Press any key to exit)");
                Console.ReadKey(true);
                Environment.Exit(0);
            }
        }
        #endregion

        #region Method: Game introduction
        public void Intro()
        {
            //Thread.Sleep(1500);

            TypeLine(ConsoleColor.Yellow, "Greetings adventurer...");

            Thread.Sleep(100); // 1 second pause between welcome and enter username prompt

            TypeLine(ConsoleColor.Yellow, "\n\nPlease enter your name: ");
            string playerName = Console.ReadLine().Trim(); // Trim helps readability for username as the game calls the username
            Player1 = new Player(playerName, 20, 1, true); // Instanciates a new player

            SituationPrompt(playerName);
        }
        #endregion

        #region Method: Situation prompt
        public void SituationPrompt(string playerName)
        {
            Console.Clear();
            Thread.Sleep(1500);
            TypeLine(ConsoleColor.Yellow, $"Zoo keeper: {playerName}, we need your help!");
            TypeLine(ConsoleColor.Yellow, "\nZoo keeper: There was a radioactive spill that contaminated the monkey exhibit at our local zoo...");
            Thread.Sleep(1500);
            TypeLine(ConsoleColor.Yellow, "\nZoo keeper: The mutated monkeys have gone on a rampage and we need someone to help us stop them!"); ;
            TypeLine(ConsoleColor.Yellow, "\nZoo keeper: Will you help us?");
            Thread.Sleep(1500);
            TypeLine(ConsoleColor.Green, "\n\nType \"YES\" to help the Zoo keeper..");
            TypeLine(ConsoleColor.Red, "\n\nType \"NO\" to exit...");
            Console.WriteLine();

            string userChoice = Console.ReadLine().ToUpper();

            if (userChoice == "YES")
            {
                Console.Clear();

                TypeLine(ConsoleColor.Yellow, "Zoo keeper: Here comes your first opponent! Get ready!");
                Thread.Sleep(1000);

                Console.Clear();
                GameLoop(); // If user chooses to help zoo keeper.. They enter the game loop.
            }
            else // Add exception handling possibly
            {
                Console.Clear();
                Console.WriteLine("(Press any key to exit)");
                Console.ReadKey(true);
                Environment.Exit(0);
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

                if (Player1.IsDead)
                {
                    ChangeColor(ConsoleColor.DarkRed, $"\nOh dear! You're dead!");
                    Thread.Sleep(2000);
                    ChangeColor(ConsoleColor.Yellow, "\n(Press any key to return to the main menu)");
                    Console.ReadKey(true);
                    Console.Clear();
                    MainMenu();
                }
                else if (TheMonkey.IsDead && i < 4) // Will cycle through victory progresion method until index hits 4
                {
                    ChangeColor(ConsoleColor.DarkRed, $"\n{TheMonkey.Name} has died!");

                    ChangeColor(ConsoleColor.Yellow, "\n(Press any key to continue)");
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
            while (TheMonkey.IsAlive && Player1.IsAlive) // Will loop through combat while monkey and player are alive
            {
                Console.Clear();
                TheMonkey.DisplayInfo(); // Display current enemy monkey info/health
                Player1.DisplayInfo(); // Displayer current player info/health
                Console.WriteLine();

                Player1.Battle(TheMonkey); // Starts with player attacking monkey
                Thread.Sleep(1000);

                TheMonkey.Battle(Player1);// Monkey attacks back
                Thread.Sleep(1000);

                if (TheMonkey.IsDead || Player1.IsDead) // Checks to see if player/enemy are dead after combat
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
            TypeLine(ConsoleColor.Yellow, $"Zoo keeper: Well done! You've defeated {TheMonkey.Name} and recieve {TheMonkey.ExpValue} exp!");

            double expGain = TheMonkey.ExpValue; // Sets monkey exp value to the exp that the player will gain for killing monkey
            Player1.TotalExperience += expGain; // Tracks player's total exp
            Thread.Sleep(1000);


            if (Player1.TotalExperience >= Player1.ExperienceToNextLevel) // Player will level up if they've met experience requirement
            {
                Player1.LevelUp();
                NextFightPrompt();
            }
            else
            {
                NextFightPrompt(); // If player doesn't earn 100 exp they won't level up and will continue to the next fight
            }
        }
        #endregion

        #region Method: Next fight prompt
        private void NextFightPrompt()
        {
            TypeLine(ConsoleColor.Yellow, "\nZoo keeper: Get ready for your next fight!");

            Thread.Sleep(1000);
            ChangeColor(ConsoleColor.Yellow, "\n\n(Press any key to continue)"); // Will continue to next fight when a key is pressed
            Console.ReadKey(true);
        }
        #endregion

        #region Method: Winner prompt
        private void WinTheGame()
        {
            Console.Clear();
            TypeLine(ConsoleColor.Yellow, $"Zoo keeper: Great job {Player1.Name}!");
            TypeLine(ConsoleColor.Yellow, $"\nZoo keeper: You managed to save the zoo by defeating all of the contaminated monkeys!");
            TypeLine(ConsoleColor.Yellow, $"\nZoo keeper: Thank you so much for your help!");
            Thread.Sleep(1000);
            ChangeColor(ConsoleColor.Yellow, "\n(Press any key to return to the main menu)");
            Console.ReadKey(true);
            Console.Clear();
            MainMenu();
        }
        #endregion

        #region Method: Change text color
        public static void ChangeColor(ConsoleColor color, string message)
        {
            Console.ForegroundColor = color;

            Console.WriteLine(message);

            Console.ResetColor();
        }
        #endregion

        #region Method: Type out lines effect
        static void TypeLine(ConsoleColor color, string line) // Method to create "typing" visual effect onto console for story line
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
    }
}

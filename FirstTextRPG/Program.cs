namespace FirstTextRPG
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // TODO add exception handling for window adjustments
            Console.Title = "Monkey Mayhem"; // Set title
            Console.WindowHeight = 45;
            Console.WindowWidth = 150;

            Game newGame = new Game();
            newGame.MainMenu();
        }
    }
    /* Possible TODOS
     * Create a combat class to handle combat logic and pass values back to the game
     * Adjust methods to return values so they're not all void
     * Adjust variables so they aren't global
     * Possibly create a GameData class to generalize data passed as input to methods / out of methods
     * Figure out how to create/instantiate enemy list outside of Game class to add flexability
     * Refactor game class so it's not as messy
     */
}
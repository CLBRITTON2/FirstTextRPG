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
    /* Posible TODOS
     * Adjust methods to return values so they're not all void
     * Adjust variables so they aren't global
     * Possibly create a seperate class to handle combat logic
     * Possibly create a GameData class to generalize data passed as input to methods / out of methods
     * Figure out how to create/instantiate enemy list outside of Game class to add flexability
     * Refactor game class so it's not a complete mess
     */
}
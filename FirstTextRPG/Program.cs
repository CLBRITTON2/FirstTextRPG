namespace FirstTextRPG
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Monkey Mayhem"; // Set title
            Console.WindowHeight = 45;
            Console.WindowWidth = 150;


            Game newGame = new Game();
            newGame.MainMenu();
            Console.WriteLine("Test1");
        }
    }
}
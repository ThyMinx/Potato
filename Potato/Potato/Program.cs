namespace Potato;
internal class Program
{
    static void Main()
    {
        while (true)
        {
            Play();
            Credits();
            if (!PlayAgain())
            {
                break;
            }
        }
    }

    private static bool PlayAgain()
    {
        Console.WriteLine("\r\n\r\nPlay again? (y/n)");
        while (true)
        {
            var input = Console.ReadKey().KeyChar.ToString().ToLower();
            if (input == "y")
            {
                return true;
            }
            else if (input == "n")
            {
                return false;
            }
        }
    }

    private static void Play()
    {
        var game = new Game();

        while (true)
        {
            game.LoadGameTitle();
            game.GrassAndMud();
            var end = game.CheckEndGame();
            if (end)
                break;
        }
    }

    private static void Credits()
    {
        Console.BackgroundColor = ConsoleColor.White;
        Console.ForegroundColor = ConsoleColor.DarkBlue;
        Console.Write("\r\nThank you for playing Potato! A one page RPG by Oliver Darkshire (");

        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write("@deathbybadger on Twitter");

        Console.ForegroundColor = ConsoleColor.DarkBlue;
        Console.Write("). You can find other one page RPGs available on Oliver's patreon for free, however, " +
            "I would say to subscribe to help support him for producing this" +
            " content for everyone to enjoy.");

        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.BackgroundColor = ConsoleColor.Black;
        Console.WriteLine("\r\n(Press any key to continue!)");

        Console.ResetColor();
        Console.ReadKey();
    }
}
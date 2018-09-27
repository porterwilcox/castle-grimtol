using System;
using System.Threading;
using CastleGrimtol.Project;

namespace CastleGrimtol
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Color color = new Color();
            string name = "";
            while(true)
            {
                System.Console.WriteLine("What's your name?");
                name = Console.ReadLine();
                if(name.Length < 1)
                {
                    continue;
                }
                Console.Clear();
                break;
            }
            Player player = new Player(name);
            System.Console.Write("Welcome to:\n\n");
            color.Red($"{name}'s Halloween Adventure!");
            Thread.Sleep(1000);
            System.Console.WriteLine("\n\nYou are 12 years old. It's Halloween night and you're grounded because you wouldn't stop scaring your little sister with your Halloween costume. You're stuck in your room thinking about all the candy you're missing out on.");
            Thread.Sleep(5000);
            System.Console.WriteLine("\nYou decide to do something about this. Do you want to play? (Y/N)");
            if(Console.ReadLine().ToLower().Contains("n"))
            {
                return;
            }
            Game game = new Game();
            game.StartGame(player);
        }
    }
}

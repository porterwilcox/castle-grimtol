using System;

namespace CastleGrimtol.Project
{
    public class Color
    {
        public void Red(string text)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(text);
            Console.ForegroundColor = ConsoleColor.Green;
        }
        public void Cyan(string text)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(text);
            Console.ForegroundColor = ConsoleColor.Green;
        }
        public void Magenta(string text)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write(text);
            Console.ForegroundColor = ConsoleColor.Green;
        }
        public void White(string text)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(text);
            Console.ForegroundColor = ConsoleColor.Green;
        }
        public void Yellow(string text)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(text);
            Console.ForegroundColor = ConsoleColor.Green;
        }
    }
}
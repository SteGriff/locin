using System;

namespace locin
{
    public class ConsoleOutputStrategy : IOutputStrategy
    {
        public void Finalise()
        {
            // No-op for Console
        }

        public void Output(string format, ConsoleColor colour, params object[] args)
        {
            Console.ForegroundColor = colour;
            Console.WriteLine(format, args);
            Console.ResetColor();
        }

        public void Output(string format, params object[] args)
        {
            Console.WriteLine(format, args);
        }

        public void Output(string text)
        {
            Console.WriteLine(text);
        }

        public void Output(string text, ConsoleColor colour)
        {
            Console.ForegroundColor = colour;
            Console.WriteLine(text);
            Console.ResetColor();
        }
    }
}

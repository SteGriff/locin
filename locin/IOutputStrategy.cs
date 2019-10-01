using System;

namespace locin
{
    interface IOutputStrategy
    {
        void Output(string format, ConsoleColor colour, params object[] args);

        void Output(string format, params object[] args);

        void Output(string text);

        void Output(string text, ConsoleColor colour);

        void Finalise();

    }
}

using System;
using System.IO;

namespace locin
{
    public class FileOutputStrategy : IOutputStrategy
    {
        private readonly StreamWriter fileWriter;

        public FileOutputStrategy(string outFile)
        {
            var fileHandle = new FileStream(outFile, FileMode.OpenOrCreate);
            fileWriter = new StreamWriter(fileHandle)
            {
                AutoFlush = true
            };
        }

        public void Finalise()
        {
            fileWriter.Flush();
            fileWriter.Close();
        }

        public void Output(string format, ConsoleColor colour, params object[] args)
        {
            fileWriter.WriteLine(format, args);
        }

        public void Output(string format, params object[] args)
        {
            fileWriter.WriteLine(format, args);
        }

        public void Output(string text)
        {
            fileWriter.WriteLine(text);
        }

        public void Output(string text, ConsoleColor colour)
        {
            fileWriter.WriteLine(text);
        }
    }
}

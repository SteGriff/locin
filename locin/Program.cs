using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace locin
{
    class Program
    {
        static Options options = new Options();
        static List<string> outputFileLines;

        static void Main(string[] args)
        {
            outputFileLines = new List<string>();

            if (!CommandLine.Parser.Default.ParseArguments(args, options))
            {
                Environment.Exit(0);
            }

            var filters = new List<string> { options.filters };
            if (options.filters.Contains(";"))
            {
                filters = options.filters.Split(new[] { ';' }).ToList();
            }

            //Prepare file filters and options,
            // figure out which files to read
            var filesToRead = new List<string>();
            var searchOption = options.recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
            string path = Environment.CurrentDirectory;
            foreach (var filter in filters)
            {
                try
                {
                    var files = Directory.EnumerateFiles(path, filter, searchOption);
                    filesToRead.AddRange(files);
                }
                catch (Exception)
                {
                    Output("Invalid filter, {0}", ConsoleColor.Red, filter);
                }

            }

            //Count lines in the target files
            var linesByFile = new Dictionary<string, int>();
            int totalLines = 0;
            int pathLength = path.Length;
            foreach (var file in filesToRead)
            {
                int lines = 0;
                if (options.includeEmpty)
                {
                    lines = File.ReadLines(file).ToList().Count;
                }
                else
                {
                    lines = File.ReadLines(file).Count(l => !string.IsNullOrWhiteSpace(l));
                }

                linesByFile.Add(file.Substring(pathLength), lines);
                totalLines += lines;
            }

            int filesCount = filesToRead.Count;
            double averageLinesPerFile = (double)totalLines / (double)filesCount;

            //Output the results
            Output("locin - {0}", path);
            Output("Number of files: {0}", filesCount); 
            Output("Total lines: {0}", totalLines);
            Output("Avg. lines per file: {0:F2}", averageLinesPerFile); 
            Output("Breakdown:");
            Output("==========");
            foreach (var file in linesByFile.OrderByDescending(f => f.Value))
            {
                Output("{0,-9}{1}", file.Value, file.Key);
            }

            Output("==========");
            Output("Total lines: {0}", totalLines);

            if (options.outfile != null)
            {
                try
                {
                    File.WriteAllLines(options.outfile, outputFileLines);
                }
                catch (Exception)
                {
                    //Strict fallback to console for error message
                    Console.WriteLine("Couldn't write to output file.");
                }

            }

        }

        private static void Output(string format, ConsoleColor colour, params object[] args)
        {
            if (options.outfile == null)
            {
                Console.ForegroundColor = colour;
                Console.WriteLine(format, (object[])args);
                Console.ResetColor();
            }
            else
            {
                outputFileLines.Add(string.Format(format, (object[])args));
            }
        }

        private static void Output(string format, params object[] args)
        {
            if (options.outfile == null)
            {
                Console.WriteLine(format, (object[])args);
            }
            else
            {
                outputFileLines.Add(string.Format(format, (object[])args));
            }
        }

        private static void Output(string text)
        {
            if (options.outfile == null)
            {
                Console.WriteLine(text);
            }
            else
            {
                outputFileLines.Add(text);
            }
        }

        private static void Output(string text, ConsoleColor colour)
        {
            if (options.outfile == null)
            {
                Console.ForegroundColor = colour;
                Console.WriteLine(text);
                Console.ResetColor();
            }
            else
            {
                outputFileLines.Add(text);
            }
        }

    }
}

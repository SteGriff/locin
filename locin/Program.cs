using CommandLine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace locin
{
    class Program
    {
        private static IOutputStrategy outputStrategy;

        static void Main(string[] args)
        {
            try
            {
                Parser.Default.ParseArguments<Options>(args).WithParsed(o => Run(o));
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Oops - locin died! Here's the exception:");
                Console.Error.WriteLine(ex.ToString());
            }
            
        }

        static void Run(Options options)
        {
            if (options.OutputFile == null)
            {
                outputStrategy = new ConsoleOutputStrategy();
            }
            else
            {
                outputStrategy = new FileOutputStrategy(options.OutputFile);
            }

            var filters = new List<string> { options.Filters };
            if (options.Filters.Contains(";"))
            {
                filters = options.Filters.Split(new[] { ';' }).ToList();
            }

            //Prepare file filters and options,
            // figure out which files to read
            var filesToRead = new List<string>();
            var searchOption = options.Recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;

            string path = Environment.CurrentDirectory;
            if (options.Path != null || Directory.Exists(options.Path))
            {
                path = options.Path;
            }

            foreach (var filter in filters)
            {
                try
                {
                    var files = Directory.EnumerateFiles(path, filter, searchOption);
                    filesToRead.AddRange(files);
                }
                catch (Exception)
                {
                    outputStrategy.Output("Invalid filter, {0}", ConsoleColor.Red, filter);
                }

            }

            //Count lines in the target files
            var linesByFile = new Dictionary<string, int>();
            int totalLines = 0;
            int pathLength = path.Length;
            foreach (var file in filesToRead)
            {
                int lines = 0;
                if (options.IncludeEmptyLines)
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
            decimal averageLinesPerFile = (decimal)totalLines / filesCount;

            //Output the results
            outputStrategy.Output("locin - {0}",  path);
            outputStrategy.Output("Number of files: {0}",  filesCount);
            outputStrategy.Output("Total lines: {0}",  totalLines);
            outputStrategy.Output("Avg. lines per file: {0:F2}",  averageLinesPerFile);
            outputStrategy.Output("Breakdown:");
            outputStrategy.Output("==========");
            foreach (var file in linesByFile.OrderByDescending(f => f.Value))
            {
                outputStrategy.Output("{0,-9}{1}", file.Value, file.Key);
            }

            outputStrategy.Output("==========");
            outputStrategy.Output("Total lines: {0}", totalLines);

            outputStrategy.Finalise();
        }

    }
}

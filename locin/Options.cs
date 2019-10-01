using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;
using CommandLine.Text;

namespace locin
{
    class Options
    {
        [Option('f', "filters", Default = "*.*", HelpText = "File name/type filters E.g. *.cs;*.xml")]
        public string Filters { get; set; }

        [Option('r', "recursive", Default = true, HelpText = "Get files recursively rather than using the top directory only.")]
        public bool Recursive { get; set; }

        [Option('e', "include-empty", Default = false, HelpText = "Include empty lines in count")]
        public bool IncludeEmptyLines { get; set; }

        [Option('o', "outfile", Default = null, HelpText = "Output results to this file path instead of printing to console")]
        public string OutputFile { get; set; }

        [Option('p', "path", HelpText = "The path to scan. Defaults to current working directory.")]
        public string Path { get; set; }
    }
}

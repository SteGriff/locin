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
        [Option('f', "filters", DefaultValue = "*.*", HelpText = "File name/type filters E.g. *.cs;*.xml")]
        public string filters { get; set; }

        [Option('r', "recursive", DefaultValue = true, HelpText = "Get files recursively rather than using the top directory only.")]
        public bool recursive { get; set; }

        [Option('e', "include-empty", DefaultValue = false, HelpText = "Include empty lines in count")]
        public bool includeEmpty { get; set; }

        [Option('o', "outfile", DefaultValue = null, HelpText = "Output results to this file path instead of printing to console")]
        public string outfile { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this, (HelpText current) => HelpText.DefaultParsingErrorsHandler(this, current));
        }
    }
}

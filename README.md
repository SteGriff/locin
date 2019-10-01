# locin

To answer the question, "How many Lines Of Code IN this thing?"

## Download

Go to [Releases](https://github.com/SteGriff/locin/releases) and download `locin.zip` from the latest release.

## How to use

Run it from the directory you want to analyse or use the `-p` parameter to specify another directory to analyse.

Options:

~~~~
-f, --filters          (Default: *.*) File name/type filters E.g. *.cs;*.xml

-r, --recursive        (Default: true) Get files recursively rather than using the top directory only.

-e, --include-empty    (Default: false) Include empty lines in count

-o, --outfile          Output results to this file path instead of printing to console

-p, --path             The path to scan. Defaults to current working directory.

--help                 Display this help screen.

--version              Display version information.
~~~~

## Filters

To make filters work in your shell, you will probably have to wrap them all together in speech marks: `locin.exe -f "*.cs;*.cshtml;*.aspx"

## Examples

~~~~
$ locin.exe -f *.cs
$ locin.exe -f "*.js;*.json;*.vue" -o loc.txt
~~~~

## Output

Running locin on itself:

~~~~
$ .\bin\Release\locin.exe -f *.cs
locin - C:\git\locin\locin
Number of files: 6
Total lines: 231
Avg. lines per file: 38.50
Breakdown:
==========
95       \Program.cs
38       \FileOutputStrategy.cs
32       \Properties\AssemblyInfo.cs
31       \ConsoleOutputStrategy.cs
23       \Options.cs
12       \IOutputStrategy.cs
==========
Total lines: 231
~~~~

## License

UNLICENSE
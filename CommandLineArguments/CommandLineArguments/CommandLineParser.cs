using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using CommandLineArguments.DTOs;

namespace CommandLineArguments
{
    internal class CommandLineParser
    {
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        private readonly CommandLineArgument helpArgument = new CommandLineArgument
        {
            shortName = "?",
            longName = "help",
            description = "Display this help message",
            numberOfArguments = 0
        };

        private readonly CommandLineArgument[] arguments;

        #region Constructors

        private CommandLineParser()
        {
        }

        public CommandLineParser(CommandLineArgument[] arguments)
        {
            if(arguments == null)
            {
                throw new ArgumentNullException(nameof(arguments));
            }

            if(arguments.Length == 0)
            {
                throw new ArgumentException("No arguments specified.", nameof(arguments));
            }

            this.arguments = arguments;
        }

        #endregion

        public Dictionary<string, string> TryParseArguments(string[] arguments, out string errorMessage)
        {
            Dictionary<string, string> results = new Dictionary<string, string>();

            string argumentToParse;
            CommandLineArgument? argument;
            for (int i = 0; i < arguments.Length; i++)
            {
                argumentToParse = arguments[i];

                if (!TryParseArgumentName(argumentToParse, out argument))
                {
                    errorMessage = "Invalid argument: " + argumentToParse;
                    errorMessage += UsageAsString();

                    return null;
                }

                // Certain of the command line arguments can be handled from within this parser.
                if (argument?.shortName == "?")
                {
                    errorMessage = UsageAsString();

                    return null;
                }

                //TODO: We only support 0 or 1 arguments at the moment.
                if (argument?.numberOfArguments == 1)
                {
                    if (i + 1 >= arguments.Length)
                    {
                        errorMessage = "Parameter expected for argument: " + argumentToParse;
                        errorMessage += UsageAsString();

                        return null;
                    }

                    results.Add(argument?.longName, arguments[i + 1]);
                    i += 1;
                }
                else
                {
                    results.Add(argument?.longName, "");
                }
            }

            errorMessage = String.Empty;
            return results;
        }

        private bool TryParseArgumentName(string argumentToParse, out CommandLineArgument? argument)
        {
            if (argumentToParse.Length < 2)
            {
                // The length should be greater than 1 to accomodate the '-' character and the
                //  argument
                argument = null;
                return false;
            }

            if (argumentToParse[0] == '/')
            {
                string shortName = argumentToParse.Substring(1);
                argument = ArgumentForShortName(shortName);

                return argument != null;
            }
            else
            {
                if (argumentToParse[0] == '-')
                {
                    if (argumentToParse[1] == '-')
                    {
                        string longName = argumentToParse.Substring(2);
                        argument = ArgumentForLongName(longName);

                        return argument != null;
                    }

                    string shortName = argumentToParse.Substring(1);
                    argument = ArgumentForShortName(shortName);

                    return argument != null;
                }
            }

            // Invalid prefix specified
            argument = null;
            return false;
        }

        private CommandLineArgument? ArgumentForShortName(string shortName)
        {
            if(helpArgument.shortName == shortName)
            {
                return helpArgument;
            }

            foreach (CommandLineArgument argument in arguments)
            {
                if (argument.shortName == shortName)
                {
                    return argument;
                }
            }

            return null;
        }

        private CommandLineArgument? ArgumentForLongName(string longName)
        {
            if (helpArgument.longName == longName)
            {
                return helpArgument;
            }

            foreach (CommandLineArgument argument in arguments)
            {
                if (string.Compare(argument.longName, longName, true) == 0)
                {
                    return argument;
                }
            }

            return null;
        }

        private string UsageAsString()
        {
            int widthOfColumnForArguments = DetermineWidthOfColumnForArguments();
            // Account for the space between the argument name and the description
            widthOfColumnForArguments += 2;

            int widthOfConsole = DetermineWidthOfConsole();
            int widthOfColumnForDescriptions = widthOfConsole - widthOfColumnForArguments - 2;

            string output = "Usage:" + Environment.NewLine;
            output += Assembly.GetExecutingAssembly().GetName().Name + " [args]" + Environment.NewLine;
            output += Environment.NewLine;

            output += "where args can be one or more of the following:" + Environment.NewLine;

            output += FormatArgumentForConsoleOutput(widthOfColumnForArguments, widthOfColumnForDescriptions, helpArgument) + Environment.NewLine;
            output += Environment.NewLine;

            foreach (CommandLineArgument argument in arguments)
            {
                output += FormatArgumentForConsoleOutput(widthOfColumnForArguments, widthOfColumnForDescriptions, argument) + Environment.NewLine;
                output += Environment.NewLine;
            }

            output += Environment.NewLine;

            return output;
        }

        private static int DetermineWidthOfConsole()
        {
            if (GetConsoleWindow() != IntPtr.Zero)
            {
                return Console.WindowWidth;
            }

            return -1;
        }

        private int DetermineWidthOfColumnForArguments()
        {
            int width = 0;

            if(!String.IsNullOrEmpty(helpArgument.shortName))
            {
                width = helpArgument.shortName.Length;
            }

            if(!String.IsNullOrEmpty(helpArgument.longName))
            {
                int length = helpArgument.longName.Length;
                if(length > width)
                {
                    width = length;
                }
            }

            foreach (CommandLineArgument argument in arguments)
            {
                if (!String.IsNullOrEmpty(argument.shortName))
                {
                    int length = argument.shortName.Length;
                    if (length > width)
                    {
                        width = length;
                    }
                }

                if (!String.IsNullOrEmpty(argument.longName))
                {
                    int length = argument.longName.Length;
                    if (length > width)
                    {
                        width = length;
                    }
                }
            }

            return width;
        }

        private static string FormatArgumentForConsoleOutput(int widthOfColumnForArguments, int widthOfColumnForDescriptions, CommandLineArgument argument)
        {
            string output = "";
            bool hasShortName = !String.IsNullOrEmpty(argument.shortName);
            bool hasLongName = !String.IsNullOrEmpty(argument.longName);

            if (hasShortName)
            {
                output += $"/{argument.shortName.PadRight(widthOfColumnForArguments)} ";
            }

            int numberOfLines = (int)Math.Ceiling((double)argument.description.Length / widthOfColumnForDescriptions);
            int startIndex = 0;
            int lengtOfLine = 0;
            if(widthOfColumnForDescriptions < argument.description.Length)
            {
                lengtOfLine = widthOfColumnForDescriptions;

                while(argument.description[startIndex+ lengtOfLine] != ' ')
                {
                    lengtOfLine--;
                }
            }
            else
            {
                lengtOfLine = argument.description.Length;
            }

            output += argument.description.Substring(startIndex, lengtOfLine) + Environment.NewLine;

            if(hasLongName)
            {
                output += $"/{argument.longName.PadRight(widthOfColumnForArguments)} ";
            }

            if(numberOfLines > 1)
            {
                if(!hasLongName)
                {
                    output += "".PadRight(widthOfColumnForArguments + 2);
                }

                startIndex += lengtOfLine;
                if (argument.description[startIndex] == ' ')
                {
                    if (startIndex + 1 < argument.description.Length)
                    {
                        startIndex++;
                    }
                }
                if (startIndex + widthOfColumnForDescriptions < argument.description.Length)
                {
                    lengtOfLine = widthOfColumnForDescriptions;
                    while (argument.description[startIndex + lengtOfLine] != ' ')
                    {
                        lengtOfLine--;
                    }
                }
                else
                {
                    lengtOfLine = argument.description.Length-startIndex;
                }
                output += argument.description.Substring(startIndex, lengtOfLine) + Environment.NewLine;

                for(int i=2; i < numberOfLines; i++)
                {
                    output += "".PadRight(widthOfColumnForArguments + 2);

                    startIndex += lengtOfLine;
                    // Cater for the space before the word we are printing as we may not have trimmed it.
                    if (argument.description[startIndex] == ' ')
                    {
                        if(startIndex+1 < argument.description.Length)
                        {
                            startIndex++;
                        }
                    }
                    if (startIndex + widthOfColumnForDescriptions < argument.description.Length)
                    {
                        lengtOfLine = widthOfColumnForDescriptions;
                        while (argument.description[startIndex + lengtOfLine] != ' ')
                        {
                            lengtOfLine--;
                        }
                    }
                    else
                    {
                        lengtOfLine = argument.description.Length - startIndex;
                    }
                    output += argument.description.Substring(startIndex, lengtOfLine) + Environment.NewLine;
                }
            }

            return output;
        }
    }
}
